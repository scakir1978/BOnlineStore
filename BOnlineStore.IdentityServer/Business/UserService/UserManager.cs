using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Extensions;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.IdentityServer.Business.UserService
{
    public class UserManager : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public UserManager(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper, IStringLocalizer<Language> stringLocalizer)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<(UserDto User, IdentityResult Result)> CreateAsync(UserCreateDto userCreateDto)
        {
            try
            {
                // Kiracý var mý kontrol et
                var tenantExists = await _context.Tenant.AnyAsync(t => t.Id == userCreateDto.TenantId);
                if (!tenantExists)
                {
                    var error = IdentityResult.Failed(new IdentityError 
                    { 
                        Code = "TenantNotFound", 
                        Description = _stringLocalizer[IdentityServerKeys.TenantNotFound]
                    });
                    return (null, error);
                }

                // Extension method ile DTO'yu Entity'ye dönüþtür
                var user = userCreateDto.ToEntity(_mapper);

                // Kullanýcýyý oluþtur
                var result = await _userManager.CreateAsync(user, userCreateDto.Password);

                if (result.Succeeded)
                {
                    var userDto = user.ToDto(_mapper);
                    return (userDto, result);
                }

                return (null, result);
            }
            catch (Exception ex)
            {
                var error = IdentityResult.Failed(new IdentityError 
                { 
                    Code = "CreateUserError", 
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.CreateUserError], ex.Message)
                });
                return (null, error);
            }
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return user.SafeMap<ApplicationUser, UserDto>(_mapper);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user.SafeMap<ApplicationUser, UserDto>(_mapper);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<(UserDto User, IdentityResult Result)> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userUpdateDto.Id);
                if (user == null)
                {
                    var error = IdentityResult.Failed(new IdentityError 
                    { 
                        Code = "UserNotFound", 
                        Description = _stringLocalizer[IdentityServerKeys.UserNotFound]
                    });
                    return (null, error);
                }

                // Extension method ile partial update uygula
                userUpdateDto.PartialUpdate(user, _mapper);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var userDto = user.ToDto(_mapper);
                    return (userDto, result);
                }

                return (null, result);
            }
            catch (Exception ex)
            {
                var error = IdentityResult.Failed(new IdentityError 
                { 
                    Code = "UpdateUserError", 
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.UpdateUserError], ex.Message)
                });
                return (null, error);
            }
        }

        public async Task<IdentityResult> DeleteAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError 
                    { 
                        Code = "UserNotFound", 
                        Description = _stringLocalizer[IdentityServerKeys.UserNotFound]
                    });
                }

                return await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Code = "DeleteUserError", 
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.DeleteUserError], ex.Message)
                });
            }
        }

        public async Task<List<UserDto>> GetUsersByTenantIdAsync(Guid tenantId)
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.TenantId == tenantId)
                    .ToListAsync();

                return users.ToDto(_mapper);
            }
            catch (Exception)
            {
                return new List<UserDto>();
            }
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return users.ToDto(_mapper);
            }
            catch (Exception)
            {
                return new List<UserDto>();
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError 
                    { 
                        Code = "UserNotFound", 
                        Description = _stringLocalizer[IdentityServerKeys.UserNotFound]
                    });
                }

                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Code = "ChangePasswordError", 
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.ChangePasswordError], ex.Message)
                });
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError 
                    { 
                        Code = "UserNotFound", 
                        Description = _stringLocalizer[IdentityServerKeys.UserNotFound]
                    });
                }

                // Reset token oluþtur ve þifreyi sýfýrla
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                return await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Code = "ResetPasswordError", 
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.ResetPasswordError], ex.Message)
                });
            }
        }
    }
}