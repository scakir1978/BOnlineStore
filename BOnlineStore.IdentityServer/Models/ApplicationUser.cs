// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BOnlineStore.IdentityServer.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public Guid TenantId { get; set; }
    [StringLength(10)]
    public string Locale { get; set; }
    public virtual Tenant Tenant { get; set; }
    
}
