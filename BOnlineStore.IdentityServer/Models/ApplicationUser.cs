// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public Guid TenantId { get; set; }

    /// <summary>
    /// Kullanıcının dil/bölge ayarları
    /// </summary>
    [StringLength(10)]
    public string Locale { get; set; }

    /// <summary>
    /// Kullanıcının tam adı
    /// </summary>
    [StringLength(256)]
    public string Name { get; set; }

    /// <summary>
    /// Kullanıcının soyadı
    /// </summary>
    [StringLength(256)]
    public string FamilyName { get; set; }

    /// <summary>
    /// Kullanıcının adı
    /// </summary>
    [StringLength(256)]
    public string GivenName { get; set; }

    /// <summary>
    /// Kullanıcının orta adı
    /// </summary>
    [StringLength(256)]
    public string MiddleName { get; set; }

    /// <summary>
    /// Kullanıcının takma adı
    /// </summary>
    [StringLength(256)]
    public string Nickname { get; set; }

    /// <summary>
    /// Tercih edilen kullanıcı adı
    /// </summary>
    [StringLength(256)]
    public string PreferredUsername { get; set; }

    /// <summary>
    /// Kullanıcının profil sayfası URL'i
    /// </summary>
    [StringLength(512)]
    public string Profile { get; set; }

    /// <summary>
    /// Kullanıcının profil resmi URL'i
    /// </summary>
    [StringLength(512)]
    public string Picture { get; set; }

    /// <summary>
    /// Kullanıcının kişisel web sitesi
    /// </summary>
    [StringLength(512)]
    public string Website { get; set; }

    /// <summary>
    /// Kullanıcının cinsiyeti
    /// </summary>
    [StringLength(10)]
    public string Gender { get; set; }

    /// <summary>
    /// Kullanıcının doğum tarihi
    /// </summary>
    public DateTime? Birthdate { get; set; }

    /// <summary>
    /// Kullanıcının zaman dilimi bilgisi
    /// </summary>
    [StringLength(50)]
    public string ZoneInfo { get; set; }

    /// <summary>
    /// Profil bilgilerinin son güncellenme zamanı
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; }
    
}
