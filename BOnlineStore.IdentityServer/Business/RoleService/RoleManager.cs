using AutoMapper;
using BOnlineStore.IdentityServer.Dtos.Role;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;
using System.Security.Claims;

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
        public async Task<(RoleDto Role, IdentityResult Result)> CreateAsync(RoleCreateDto roleCreateDto)
        {
            try
            {
                // Check if role already exists
                var existingRole = await _roleManager.FindByNameAsync(roleCreateDto.Name);
                if (existingRole != null)
                {
                    return (null, IdentityResult.Failed(new IdentityError
                    {
                        Code = nameof(IdentityServerKeys.RoleAlreadyExists),
                        Description = _stringLocalizer[IdentityServerKeys.RoleAlreadyExists]
                    }));
                }

                var role = new IdentityRole
                {
                    Name = roleCreateDto.Name,
                    NormalizedName = roleCreateDto.Name?.ToUpperInvariant()
                    // ConcurrencyStamp otomatik olarak _roleManager tarafýndan ayarlanýr
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return (_mapper.Map<RoleDto>(role), result);
                }

                return (null, result);
            }
            catch (Exception ex)
            {
                var error = IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(IdentityServerKeys.CreateRoleError),
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.CreateRoleError], ex.Message)
                });
                return (null, error);
            }
        }

        /// <summary>
        /// Var olan rolü günceller.
        /// </summary>
        /// <param name="roleUpdateDto">Güncellenecek rol bilgileri</param>
        /// <returns>Güncellenen rol ve iþlem sonucu</returns>
        public async Task<(RoleDto Role, IdentityResult Result)> UpdateAsync(string roleId, RoleUpdateDto roleUpdateDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return (null, IdentityResult.Failed(new IdentityError
                    {
                        Code = nameof(IdentityServerKeys.RoleNotFound),
                        Description = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                    }));
                }

                role.Name = roleUpdateDto.Name;
                role.NormalizedName = roleUpdateDto.Name?.ToUpperInvariant();
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return (_mapper.Map<RoleDto>(role), result);
                }
                return (null, result);
            }
            catch (Exception ex)
            {
                var error = IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(IdentityServerKeys.UpdateRoleError),
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.UpdateRoleError], ex.Message)
                });
                return (null, error);
            }
        }

        /// <summary>
        /// Rol siler.
        /// </summary>
        /// <param name="roleId">Silinecek rol kimliði</param>
        /// <returns>Ýþlem sonucu</returns>
        public async Task<(RoleDto Role, IdentityResult Result)> DeleteAsync(string roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return (null, IdentityResult.Failed(new IdentityError
                    {
                        Code = nameof(IdentityServerKeys.RoleNotFound),
                        Description = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                    }));
                }

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return (_mapper.Map<RoleDto>(role), result);
                }

                return (null, result);

            }
            catch (Exception ex)
            {
                return (null, IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(IdentityServerKeys.DeleteRoleError),
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.DeleteRoleError], ex.Message)
                }));
            }
        }

        /// <summary>
        /// Rolü ID ile getirir.
        /// </summary>
        /// <param name="roleId">Rol kimliði</param>
        /// <returns>Rol bilgileri</returns>
        public async Task<(RoleDto Role, IdentityResult Result)> GetByIdAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return (null, IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(IdentityServerKeys.RoleNotFound),
                    Description = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                }));
            }

            return (_mapper.Map<RoleDto>(role), IdentityResult.Success);
        }

        /// <summary>
        /// Rolü adýyla getirir.
        /// </summary>
        /// <param name="roleName">Rol adý</param>
        /// <returns>Rol bilgileri</returns>
        public async Task<(RoleDto Role, IdentityResult Result)> GetByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return (null, IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(IdentityServerKeys.RoleNotFound),
                    Description = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                }));
            }
            return (_mapper.Map<RoleDto>(role), IdentityResult.Success);
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
