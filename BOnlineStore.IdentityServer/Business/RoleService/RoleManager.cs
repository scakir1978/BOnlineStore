using AutoMapper;
using BOnlineStore.IdentityServer.Dtos.Role;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

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

        public RoleManager(RoleManager<IdentityRole> roleManager, IMapper mapper, IStringLocalizer<Language> stringLocalizer)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
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
        public async Task<(RoleDto Role, IdentityResult Result)> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleUpdateDto.Id);

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
        public async Task<IdentityResult> DeleteAsync(string roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = nameof(IdentityServerKeys.RoleNotFound),
                        Description = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                    });
                }
                return await _roleManager.DeleteAsync(role);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = nameof(IdentityServerKeys.DeleteRoleError),
                    Description = string.Format(_stringLocalizer[IdentityServerKeys.DeleteRoleError], ex.Message)
                });
            }
        }

        /// <summary>
        /// Rolü ID ile getirir.
        /// </summary>
        /// <param name="roleId">Rol kimliði</param>
        /// <returns>Rol bilgileri</returns>
        public async Task<RoleDto> GetByIdAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return _mapper.Map<RoleDto>(role);
        }

        /// <summary>
        /// Rolü adýyla getirir.
        /// </summary>
        /// <param name="roleName">Rol adý</param>
        /// <returns>Rol bilgileri</returns>
        public async Task<RoleDto> GetByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return _mapper.Map<RoleDto>(role);
        }

        /// <summary>
        /// Tüm rolleri listeler.
        /// </summary>
        /// <returns>Rol listesi</returns>
        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}
