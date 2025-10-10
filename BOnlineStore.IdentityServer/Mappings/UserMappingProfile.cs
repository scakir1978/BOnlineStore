using AutoMapper;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Dtos.User;
using BOnlineStore.IdentityServer.Models;

namespace BOnlineStore.IdentityServer.Mappings
{
    /// <summary>
    /// User entity ve DTO mappings için AutoMapper profili
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateUserMappings();
            CreateTenantMappings();
        }

        private void CreateUserMappings()
        {
            // ApplicationUser to UserDto mapping
            CreateMap<ApplicationUser, UserDto>();

            // UserDto to ApplicationUser mapping (reverse mapping)
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.Tenant, opt => opt.Ignore());

            // UserCreateDto to ApplicationUser mapping
            CreateMap<UserCreateDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Locale, opt => opt.MapFrom(src => 
                    string.IsNullOrEmpty(src.Locale) ? "tr-TR" : src.Locale))
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Identity will generate this
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Handled separately
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.Tenant, opt => opt.Ignore());

            // UserUpdateDto to ApplicationUser mapping for partial updates
            CreateMap<UserUpdateDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.TenantId, opt => opt.Ignore()) // TenantId shouldn't be updated
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.Tenant, opt => opt.Ignore())
                // Null deðerleri ignore et (sadece dolu alanlarý güncelle)
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private void CreateTenantMappings()
        {
            // Tenant mappings
            CreateMap<Tenant, TenantDto>().ReverseMap();
            CreateMap<Tenant, TenantCreateDto>().ReverseMap();
            CreateMap<Tenant, TenantUpdateDto>().ReverseMap();
        }
    }
}