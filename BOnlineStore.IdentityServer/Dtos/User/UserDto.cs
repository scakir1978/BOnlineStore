namespace BOnlineStore.IdentityServer.Dtos.User
{
    public class UserDto
    {
        /// <summary>
        /// Kullanýcý kimliði
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Kullanýcý adý
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email doðrulandý mý?
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Telefon numarasý
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Telefon numarasý doðrulandý mý?
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Ýki faktörlü kimlik doðrulama etkin mi?
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Kilit son zamaný
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Kilit etkin mi?
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Baþarýsýz giriþ sayýsý
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Kiracý kimliði
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Kullanýcýnýn dil/bölge ayarlarý
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Kullanýcýnýn tam adý
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Kullanýcýnýn soyadý
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Kullanýcýnýn adý
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Kullanýcýnýn orta adý
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Kullanýcýnýn takma adý
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Tercih edilen kullanýcý adý
        /// </summary>
        public string PreferredUsername { get; set; }

        /// <summary>
        /// Kullanýcýnýn profil sayfasý URL'i
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Kullanýcýnýn profil resmi URL'i
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Kullanýcýnýn kiþisel web sitesi
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Kullanýcýnýn cinsiyeti
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Kullanýcýnýn doðum tarihi
        /// </summary>
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Kullanýcýnýn zaman dilimi bilgisi
        /// </summary>
        public string ZoneInfo { get; set; }

        /// <summary>
        /// Profil bilgilerinin son güncellenme zamaný
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}