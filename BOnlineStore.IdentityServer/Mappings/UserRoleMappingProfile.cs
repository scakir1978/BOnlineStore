using AutoMapper;
using BOnlineStore.IdentityServer.Dtos.Role;
using BOnlineStore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace BOnlineStore.IdentityServer.Mappings
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();
        }
    }
}
