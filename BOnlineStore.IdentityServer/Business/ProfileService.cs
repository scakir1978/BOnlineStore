using BOnlineStore.IdentityServer.Models;
using BOnlineStore.IdentityServer.Settings;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BOnlineStore.IdentityServer.Business
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new Claim(IdentityServerConstants.ProfilClaimTypeTenantId, user.TenantId.ToString()),
                new Claim(IdentityServerConstants.ProfilClaimTypeLocale, user.Locale.Trim())
            };

            // Profile scope için OpenID Connect standart claim'lerini ekle
            if (!string.IsNullOrWhiteSpace(user.Name))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeName, user.Name));

            if (!string.IsNullOrWhiteSpace(user.FamilyName))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeFamilyName, user.FamilyName));

            if (!string.IsNullOrWhiteSpace(user.GivenName))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeGivenName, user.GivenName));

            if (!string.IsNullOrWhiteSpace(user.MiddleName))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeMiddleName, user.MiddleName));

            if (!string.IsNullOrWhiteSpace(user.Nickname))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeNickname, user.Nickname));

            if (!string.IsNullOrWhiteSpace(user.PreferredUsername))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypePreferredUsername, user.PreferredUsername));

            if (!string.IsNullOrWhiteSpace(user.Profile))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeProfile, user.Profile));

            if (!string.IsNullOrWhiteSpace(user.Picture))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypePicture, user.Picture));

            if (!string.IsNullOrWhiteSpace(user.Website))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeWebsite, user.Website));

            if (!string.IsNullOrWhiteSpace(user.Gender))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeGender, user.Gender));

            if (user.Birthdate.HasValue)
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeBirthdate, user.Birthdate.Value.ToString("yyyy-MM-dd")));

            if (!string.IsNullOrWhiteSpace(user.ZoneInfo))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeZoneInfo, user.ZoneInfo));

            if (!string.IsNullOrWhiteSpace(user.Locale))
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeLocale, user.Locale));

            if (user.UpdatedAt.HasValue)
                claims.Add(new Claim(IdentityServerConstants.ProfilClaimTypeUpdatedAt, user.UpdatedAt.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")));

            context.IssuedClaims.AddRange(claims);

        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);

            context.IsActive = user != null;

        }
    }
}
