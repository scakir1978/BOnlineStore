using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Extensions;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BOnlineStore.IdentityServer.Business.UserService
{
    public class UserManager : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserManager(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
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
                        Description = "Belirtilen kiracý bulunamadý." 
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
                    Description = $"Kullanýcý oluþturulurken hata oluþtu: {ex.Message}" 
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
                        Description = "Kullanýcý bulunamadý." 
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
                    Description = $"Kullanýcý güncellenirken hata oluþtu: {ex.Message}" 
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
                        Description = "Kullanýcý bulunamadý." 
                    });
                }

                return await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Code = "DeleteUserError", 
                    Description = $"Kullanýcý silinirken hata oluþtu: {ex.Message}" 
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
                        Description = "Kullanýcý bulunamadý." 
                    });
                }

                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError 
                { 
                    Code = "ChangePasswordError", 
                    Description = $"Þifre deðiþtirilirken hata oluþtu: {ex.Message}" 
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
                        Description = "Kullanýcý bulunamadý." 
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
                    Description = $"Þifre sýfýrlanýrken hata oluþtu: {ex.Message}" 
                });
            }
        }
    }
}