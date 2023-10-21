using AutoMapper;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;

namespace BOnlineStore.Services.Production.Api.Mappings
{
    public class FormulaMappings : Profile
    {
        public FormulaMappings()
        {
            CreateMap<Formula, FormulaDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Formula, FormulaCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Formula, FormulaUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}

