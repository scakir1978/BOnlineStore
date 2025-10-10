using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Models
{
    /// <summary>
    /// Þifre deðiþtirme isteði
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Kullanýcý kimliði
        /// </summary>
        [Required(ErrorMessage = "Kullanýcý kimliði gereklidir.")]
        public string UserId { get; set; }

        /// <summary>
        /// Mevcut þifre
        /// </summary>
        [Required(ErrorMessage = "Mevcut þifre gereklidir.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Yeni þifre
        /// </summary>
        [Required(ErrorMessage = "Yeni þifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Þifre en az 6 karakter olmalýdýr.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// Þifre sýfýrlama isteði
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Kullanýcý kimliði
        /// </summary>
        [Required(ErrorMessage = "Kullanýcý kimliði gereklidir.")]
        public string UserId { get; set; }

        /// <summary>
        /// Yeni þifre
        /// </summary>
        [Required(ErrorMessage = "Yeni þifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Þifre en az 6 karakter olmalýdýr.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}