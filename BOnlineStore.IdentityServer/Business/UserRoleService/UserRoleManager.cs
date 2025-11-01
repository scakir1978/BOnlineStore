using AutoMapper;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos.UserRole;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BOnlineStore.IdentityServer.Business.UserRoleService
{
    /// <summary>
    /// Kullanýcý-Rol iliþkilerini yöneten servis. Rol atama, kaldýrma ve listeleme iþlemlerini gerçekleþtirir.
    /// </summary>
    public class UserRoleManager : IUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public UserRoleManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
  ApplicationDbContext context, IStringLocalizer<Language> stringLocalizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Kullanýcýya rol atar.
        /// </summary>
        /// <param name="userRoleAssignDto">Atanacak kullanýcý-rol bilgileri</param>
        /// <returns>Atanan rol ve iþlem sonucu</returns>
        public async Task<Response<UserRoleDto>> AssignRoleToUserAsync(UserRoleAssignDto userRoleAssignDto)
        {
            try
            {
                // Kullanýcýyý kontrol et
                var user = await _userManager.FindByIdAsync(userRoleAssignDto.UserId);
                if (user == null)
                {
                    return Response<UserRoleDto>.Fail(
                        new Error { ErrorCode = nameof(IdentityServerKeys.UserNotFound), Message = _stringLocalizer[IdentityServerKeys.UserNotFound] },
                       HttpStatusCode.NotFound);
                }

                // Rolü kontrol et
                var role = await _roleManager.FindByIdAsync(userRoleAssignDto.RoleId);
                if (role == null)
                {
                    return Response<UserRoleDto>.Fail(
                        new Error { ErrorCode = nameof(IdentityServerKeys.RoleNotFound), Message = _stringLocalizer[IdentityServerKeys.RoleNotFound] },
                 HttpStatusCode.NotFound);
                }

                // Kullanýcýnýn zaten bu role sahip olup olmadýðýný kontrol et
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (isInRole)
                {
                    return Response<UserRoleDto>.Fail(
                            new Error { ErrorCode = nameof(IdentityServerKeys.UserAlreadyHasRole), Message = _stringLocalizer[IdentityServerKeys.UserAlreadyHasRole] },
                            HttpStatusCode.BadRequest);
                }

                // Rolü kullanýcýya ata
                var result = await _userManager.AddToRoleAsync(user, role.Name);

                if (result.Succeeded)
                {
                    var userRoleDto = new UserRoleDto
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                        UserName = user.UserName,
                        RoleName = role.Name
                    };
                    return Response<UserRoleDto>.Success(userRoleDto, HttpStatusCode.OK);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<UserRoleDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<UserRoleDto>.Fail(
                       new Error { ErrorCode = nameof(IdentityServerKeys.AssignRoleToUserError), Message = string.Format(_stringLocalizer[IdentityServerKeys.AssignRoleToUserError], ex.Message) },
                 HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Kullanýcýdan rol kaldýrýr.
        /// </summary>
        /// <param name="userId">Kullanýcý ID</param>
        /// <param name="roleId">Rol ID</param>
        /// <returns>Kaldýrýlan rol ve iþlem sonucu</returns>
        public async Task<Response<UserRoleDto>> RemoveRoleFromUserAsync(string userId, string roleId)
        {
            try
            {
                // Kullanýcýyý kontrol et
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Response<UserRoleDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.UserNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.UserNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Rolü kontrol et
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return Response<UserRoleDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.RoleNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.RoleNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Kullanýcýnýn bu role sahip olup olmadýðýný kontrol et
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (!isInRole)
                {
                    return Response<UserRoleDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.UserRoleNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.UserRoleNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Rolü kullanýcýdan kaldýr
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (result.Succeeded)
                {
                    var userRoleDto = new UserRoleDto
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                        UserName = user.UserName,
                        RoleName = role.Name
                    };
                    return Response<UserRoleDto>.Success(userRoleDto, HttpStatusCode.OK);
                }

                var errors = result.Errors.Select(e => new Error { ErrorCode = e.Code, Message = e.Description }).ToList();
                return Response<UserRoleDto>.Fail(errors, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Response<UserRoleDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.RemoveRoleFromUserError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.RemoveRoleFromUserError],
                        ex.Message),
                        StackTrace = ex.StackTrace
                    },
                        HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Belirli bir kullanýcýnýn rollerini getirir.
        /// </summary>
        /// <param name="userId">Kullanýcý ID</param>
        /// <returns>Kullanýcý-rol listesi</returns>
        public async Task<Response<List<UserRoleDto>>> GetUserRolesAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Response<List<UserRoleDto>>.Fail(
                       new Error
                       {
                           ErrorCode = nameof(IdentityServerKeys.UserNotFound),
                           Message = _stringLocalizer[IdentityServerKeys.UserNotFound]
                       }, HttpStatusCode.NotFound);
                }

                var roleNames = await _userManager.GetRolesAsync(user);
                var userRoles = new List<UserRoleDto>();

                foreach (var roleName in roleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        userRoles.Add(new UserRoleDto
                        {
                            UserId = user.Id,
                            RoleId = role.Id,
                            UserName = user.UserName,
                            RoleName = role.Name
                        });
                    }
                }

                return Response<List<UserRoleDto>>.Success(userRoles, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<List<UserRoleDto>>.Fail(
                   new Error
                   {
                       ErrorCode = nameof(IdentityServerKeys.GetUserRolesError),
                       Message = string.Format(_stringLocalizer[IdentityServerKeys.GetUserRolesError], ex.Message),
                       StackTrace = ex.StackTrace
                   }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tüm kullanýcý-rol iliþkilerini getirir.
        /// </summary>
        /// <returns>Tüm kullanýcý-rol listesi</returns>
        public async Task<Response<List<UserRoleDto>>> GetAllUserRolesAsync()
        {
            try
            {
                var userRoles = await _context.UserRoles.ToListAsync();
                var result = new List<UserRoleDto>();

                foreach (var userRole in userRoles)
                {
                    var user = await _userManager.FindByIdAsync(userRole.UserId);
                    var role = await _roleManager.FindByIdAsync(userRole.RoleId);

                    if (user != null && role != null)
                    {
                        result.Add(new UserRoleDto
                        {
                            UserId = user.Id,
                            RoleId = role.Id,
                            UserName = user.UserName,
                            RoleName = role.Name
                        });
                    }
                }

                return Response<List<UserRoleDto>>.Success(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<List<UserRoleDto>>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.GetUserRolesError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.GetUserRolesError], ex.Message),
                        StackTrace = ex.StackTrace
                    },
                    HttpStatusCode.InternalServerError);
            }
        }
    }
}
