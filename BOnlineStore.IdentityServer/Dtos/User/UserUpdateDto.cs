using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Dtos.User
{
    public class UserUpdateDto
    {
        /// <summary>
        /// Kullanýcý kimliði
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Email adresi
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Telefon numarasý
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Kullanýcýnýn dil/bölge ayarlarý
        /// </summary>
        [StringLength(10)]
        public string Locale { get; set; }

        /// <summary>
        /// Kullanýcýnýn tam adý
        /// </summary>
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Kullanýcýnýn soyadý
        /// </summary>
        [StringLength(256)]
        public string FamilyName { get; set; }

        /// <summary>
        /// Kullanýcýnýn adý
        /// </summary>
        [StringLength(256)]
        public string GivenName { get; set; }

        /// <summary>
        /// Kullanýcýnýn orta adý
        /// </summary>
        [StringLength(256)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Kullanýcýnýn takma adý
        /// </summary>
        [StringLength(256)]
        public string Nickname { get; set; }

        /// <summary>
        /// Tercih edilen kullanýcý adý
        /// </summary>
        [StringLength(256)]
        public string PreferredUsername { get; set; }

        /// <summary>
        /// Kullanýcýnýn profil sayfasý URL'i
        /// </summary>
        [StringLength(512)]
        public string Profile { get; set; }

        /// <summary>
        /// Kullanýcýnýn profil resmi URL'i
        /// </summary>
        [StringLength(512)]
        public string Picture { get; set; }

        /// <summary>
        /// Kullanýcýnýn kiþisel web sitesi
        /// </summary>
        [StringLength(512)]
        public string Website { get; set; }

        /// <summary>
        /// Kullanýcýnýn cinsiyeti
        /// </summary>
        [StringLength(10)]
        public string Gender { get; set; }

        /// <summary>
        /// Kullanýcýnýn doðum tarihi
        /// </summary>
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Kullanýcýnýn zaman dilimi bilgisi
        /// </summary>
        [StringLength(50)]
        public string ZoneInfo { get; set; }
    }
}