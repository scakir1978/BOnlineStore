using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.IdentityServer.Business.UserService
{
    public interface IUserService
    {
        /// <summary>
        /// Yeni kullanýcý oluþturur
        /// </summary>
        /// <param name="userCreateDto">Oluþturulacak kullanýcý bilgileri</param>
        /// <returns>Oluþturulan kullanýcý bilgileri ve iþlem sonucu</returns>
        Task<Response<UserDto>> CreateAsync(UserCreateDto userCreateDto);

        /// <summary>
        /// Kullanýcý bilgilerini günceller
        /// </summary>
        /// <param name="userUpdateDto">Güncellenecek kullanýcý bilgileri</param>
        /// <returns>Güncellenmiþ kullanýcý bilgileri ve iþlem sonucu</returns>
        Task<Response<UserDto>> UpdateAsync(UserUpdateDto userUpdateDto);

        /// <summary>
        /// Kullanýcýyý ID ile getirir
        /// </summary>
        /// <param name="userId">Kullanýcý kimliði</param>
        /// <returns>Kullanýcý bilgileri</returns>
        Task<UserDto> GetByIdAsync(string userId);

        /// <summary>
        /// Kullanýcýyý email ile getirir
        /// </summary>
        /// <param name="email">Email adresi</param>
        /// <returns>Kullanýcý bilgileri</returns>
        Task<UserDto> GetByEmailAsync(string email);

        /// <summary>
        /// Kullanýcýyý siler
        /// </summary>
        /// <param name="userId">Silinecek kullanýcý kimliði</param>
        /// <returns>Ýþlem sonucu</returns>
        Task<Response<UserDto>> DeleteAsync(string userId);

        /// <summary>
        /// Belirli bir kiracýya ait kullanýcýlarý getirir
        /// </summary>
        /// <param name="tenantId">Kiracý kimliði</param>
        /// <returns>Kullanýcý listesi</returns>
        Task<List<UserDto>> GetUsersByTenantIdAsync(Guid tenantId);

        /// <summary>
        /// Tüm kullanýcýlarý getirir
        /// </summary>
        /// <returns>Kullanýcý listesi</returns>
        Task<List<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Kullanýcýnýn þifresini deðiþtirir
        /// </summary>
        /// <param name="userId">Kullanýcý kimliði</param>
        /// <param name="currentPassword">Mevcut þifre</param>
        /// <param name="newPassword">Yeni þifre</param>
        /// <returns>Ýþlem sonucu</returns>
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        /// <summary>
        /// Kullanýcýnýn þifresini sýfýrlar (admin iþlemi)
        /// </summary>
        /// <param name="userId">Kullanýcý kimliði</param>
        /// <param name="newPassword">Yeni þifre</param>
        /// <returns>Ýþlem sonucu</returns>
        Task<IdentityResult> ResetPasswordAsync(string userId, string newPassword);
    }
}