using AutoMapper;
using BOnlineStore.IdentityServer.Dtos.Role;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BOnlineStore.IdentityServer.Business.RoleService
{
    /// <summary>
    /// Rol iþlemlerini yöneten servis. Oluþturma, güncelleme, silme ve sorgulama iþlemlerini gerçekleþtirir.
    /// </summary>
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleManager(RoleManager<IdentityRole> roleManager, IMapper mapper, IStringLocalizer<Language> stringLocalizer, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Yeni rol oluþturur.
        /// </summary>
        /// <param name="roleCreateDto">Oluþturulacak rol bilgileri</param>
        /// <returns>Oluþturulan rol ve iþlem sonucu</returns>
        public async Task<Response<RoleDto>> CreateAsync(RoleCreateDto roleCreateDto)
        {
            try
            {
                // Check if role already exists
                var existingRole = await _roleManager.FindByNameAsync(roleCreateDto.Name);
                if (existingRole != null)
                {
                    return Response<RoleDto>.Fail(new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.RoleAlreadyExists),
                        Message = _stringLocalizer[IdentityServerKeys.RoleAlreadyExists]
                    }, HttpStatusCode.BadRequest);
                }

                var role = new IdentityRole
                {
                    Name = roleCreateDto.Name,
                    NormalizedName = roleCreateDto.Name?.ToUpperInvariant()
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    var roleDto = _mapper.Map<RoleDto>(role);
                    return Response<RoleDto>.Success(roleDto, HttpStatusCode.Created);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<RoleDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<RoleDto>.Fail(
                   new Error
                   {
                       ErrorCode = nameof(IdentityServerKeys.CreateRoleError),
                       Message = string.Format(_stringLocalizer[IdentityServerKeys.CreateRoleError], ex.Message),
                       StackTrace = ex.StackTrace
                   }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Var olan rolü günceller.
        /// </summary>
        /// <param name="roleUpdateDto">Güncellenecek rol bilgileri</param>
        /// <returns>Güncellenen rol ve iþlem sonucu</returns>
        public async Task<Response<RoleDto>> UpdateAsync(string roleId, RoleUpdateDto roleUpdateDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return Response<RoleDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.RoleNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                        }, HttpStatusCode.NotFound);
                }

                role.Name = roleUpdateDto.Name;
                role.NormalizedName = roleUpdateDto.Name?.ToUpperInvariant();
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    var roleDto = _mapper.Map<RoleDto>(role);
                    return Response<RoleDto>.Success(roleDto, HttpStatusCode.OK);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<RoleDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<RoleDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.UpdateRoleError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.UpdateRoleError], ex.Message),
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Rol siler.
        /// </summary>
        /// <param name="roleId">Silinecek rol kimliði</param>
        /// <returns>Ýþlem sonucu</returns>
        public async Task<Response<RoleDto>> DeleteAsync(string roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return Response<RoleDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.RoleNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Rolü silmeden önce DTO'ya dönüþtür
                var roleDto = _mapper.Map<RoleDto>(role);

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return Response<RoleDto>.Success(roleDto, HttpStatusCode.OK);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<RoleDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<RoleDto>.Fail(
                 new Error
                 {
                     ErrorCode = nameof(IdentityServerKeys.DeleteRoleError),
                     Message = string.Format(_stringLocalizer[IdentityServerKeys.DeleteRoleError], ex.Message),
                     StackTrace = ex.StackTrace
                 }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Rolü ID ile getirir.
        /// </summary>
        /// <param name="roleId">Rol kimliði</param>
        /// <returns>Rol bilgileri</returns>
        public async Task<Response<RoleDto>> GetByIdAsync(string roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return Response<RoleDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.RoleNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                        }, HttpStatusCode.NotFound);
                }

                var roleDto = _mapper.Map<RoleDto>(role);
                return Response<RoleDto>.Success(roleDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<RoleDto>.Fail(
                       new Error
                       {
                           ErrorCode = "GetRoleByIdError",
                           Message = $"Error getting role by ID: {ex.Message}",
                           StackTrace = ex.StackTrace
                       }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Rolü adýyla getirir.
        /// </summary>
        /// <param name="roleName">Rol adý</param>
        /// <returns>Rol bilgileri</returns>
        public async Task<Response<RoleDto>> GetByNameAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    return Response<RoleDto>.Fail(
                          new Error
                          {
                              ErrorCode = nameof(IdentityServerKeys.RoleNotFound),
                              Message = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                          }, HttpStatusCode.NotFound);
                }

                var roleDto = _mapper.Map<RoleDto>(role);
                return Response<RoleDto>.Success(roleDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<RoleDto>.Fail(
                   new Error
                   {
                       ErrorCode = "GetRoleByNameError",
                       Message = $"Error getting role by name: {ex.Message}",
                       StackTrace = ex.StackTrace
                   }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tüm rolleri listeler.
        /// </summary>
        /// <returns>Rol listesi</returns>
        public async Task<List<RoleDto>> GetAllAsync()
        {
            // Mevcut kullanýcýnýn ID'sini al
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

            // Kullanýcý bulunamadýysa tüm rolleri döndür (güvenlik için)
            if (string.IsNullOrEmpty(userId))
            {
                var allRoles = await _roleManager.Roles.ToListAsync();
                return _mapper.Map<List<RoleDto>>(allRoles);
            }

            // Kullanýcýnýn bilgilerini al
            var user = await _userManager.FindByIdAsync(userId);

            // Kullanýcý bulunamadýysa tüm rolleri döndür
            if (user == null)
            {
                var allRoles = await _roleManager.Roles.ToListAsync();
                return _mapper.Map<List<RoleDto>>(allRoles);
            }

            // Kullanýcýnýn rollerini al
            var userRoles = await _userManager.GetRolesAsync(user);

            // Kullanýcý SuperUser rolünde mi kontrol et
            var isSuperUser = userRoles.Contains("SuperUser");

            // Kullanýcý Admin rolünde mi kontrol et
            var isAdminUser = userRoles.Contains("Admin");

            // SuperUser deðilse, SuperUser rolünü filtrele
            var roles = await _roleManager.Roles.ToListAsync();

            if (!isSuperUser)
            {
                roles = roles.Where(r => r.Name != "SuperUser").ToList();
            }

            //Admin deðilse ve superUser deðilse Admin rolünü filtrele
            if (!isAdminUser && !isSuperUser)
            {
                roles = roles.Where(r => r.Name != "Admin").ToList();
            }

            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}
