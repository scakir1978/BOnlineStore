using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;

namespace BOnlineStore.Services.Production.Api.Mappings
{
    public class FormulaTypeMappings : Profile
    {
        public FormulaTypeMappings()
        {
            CreateMap<FormulaType, FormulaTypeDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<FormulaType, FormulaTypeCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<FormulaType, FormulaTypeUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
