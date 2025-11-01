using AutoMapper;
using BOnlineStore.IdentityServer.Dtos.Role;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.IdentityServer.Mappings
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<IdentityRole, RoleDto>().ReverseMap();
        }
    }
}
