using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Extensions;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace BOnlineStore.IdentityServer.Business.UserService
{
    public class UserManager : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper, IStringLocalizer<Language> stringLocalizer, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
        }

        private Guid GetTenantId()
        {
            var tenantId = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == GlobalConstants.tenantId)?.Value ?? "";
            return new Guid(tenantId);
        }

        public async Task<Response<UserDto>> CreateAsync(UserCreateDto userCreateDto)
        {
            try
            {
                // TenantId'yi HttpContext'ten al
                var tenantId = GetTenantId();

                // Tenant kontrolü
                var tenantExists = await _context.Tenant.AnyAsync(t => t.Id == tenantId);
                if (!tenantExists)
                {
                    return Response<UserDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.TenantNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.TenantNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Email kontrolü
                var existingUser = await _userManager.FindByEmailAsync(userCreateDto.Email);
                if (existingUser != null)
                {
                    return Response<UserDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.EmailAlreadyExists),
                            Message = _stringLocalizer[IdentityServerKeys.EmailAlreadyExists]
                        }, HttpStatusCode.BadRequest);
                }

                var user = _mapper.Map<ApplicationUser>(userCreateDto);
                user.UserName = userCreateDto.Email;
                user.TenantId = tenantId; // HttpContext'ten alýnan TenantId'yi ata
                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userManager.CreateAsync(user, userCreateDto.Password);

                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return Response<UserDto>.Success(userDto, HttpStatusCode.Created);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<UserDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<UserDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.CreateUserError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.CreateUserError], ex.Message),
                        StackTrace = ex.StackTrace
                    },
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<Response<UserDto>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userUpdateDto.Id);
                if (user == null)
                {
                    return Response<UserDto>.Fail(
                        new Error { ErrorCode = nameof(IdentityServerKeys.UserNotFound), Message = _stringLocalizer[IdentityServerKeys.UserNotFound] },
                        HttpStatusCode.NotFound);
                }

                // Email deðiþtirilmiþse, baþka kullanýcý tarafýndan kullanýlýp kullanýlmadýðýný kontrol et
                if (!string.IsNullOrEmpty(userUpdateDto.Email) && user.Email != userUpdateDto.Email)
                {
                    var existingUser = await _userManager.FindByEmailAsync(userUpdateDto.Email);
                    if (existingUser != null && existingUser.Id != user.Id)
                    {
                        return Response<UserDto>.Fail(
                            new Error { ErrorCode = nameof(IdentityServerKeys.EmailAlreadyExists), Message = _stringLocalizer[IdentityServerKeys.EmailAlreadyExists] },
                            HttpStatusCode.BadRequest);
                    }
                }

                _mapper.Map(userUpdateDto, user);
                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return Response<UserDto>.Success(userDto, HttpStatusCode.OK);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<UserDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<UserDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.UpdateUserError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.UpdateUserError], ex.Message),
                        StackTrace = ex.StackTrace
                    },
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<UserDto>> DeleteAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Response<UserDto>.Fail(
                        new Error { ErrorCode = nameof(IdentityServerKeys.UserNotFound), Message = _stringLocalizer[IdentityServerKeys.UserNotFound] },
                        HttpStatusCode.NotFound);
                }

                // Kullanýcýyý silmeden önce DTO'ya dönüþtür
                var userDto = _mapper.Map<UserDto>(user);

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Response<UserDto>.Success(userDto, HttpStatusCode.OK);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<UserDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<UserDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.DeleteUserError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.DeleteUserError], ex.Message),
                        StackTrace = ex.StackTrace
                    },
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<UserDto>> GetUsersByTenantIdAsync(Guid tenantId)
        {
            var users = await _context.Users.Where(u => u.TenantId == tenantId).ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Code = "UserNotFound", Description = _stringLocalizer[IdentityServerKeys.UserNotFound] });
            }

            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Code = "UserNotFound", Description = _stringLocalizer[IdentityServerKeys.UserNotFound] });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}