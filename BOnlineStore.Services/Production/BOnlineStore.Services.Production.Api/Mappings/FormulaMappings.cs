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
            CreateMap<Formula, FormulaLoadDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Formula, FormulaCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<FormulaDetail, FormulaDetailDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Formula, FormulaUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation()
                .ForMember(dest => dest.Code, opt => opt.Condition(src => (src.Code != null)))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != null)))
                .ForMember(dest => dest.FormulaSort, opt => opt.Condition(src => (src.FormulaSort != null)))
                .ForMember(dest => dest.FormulaDetails, opt => opt.Condition(src => (src.FormulaDetails != null)))
                .ForMember(dest => dest.RawMaterialId, opt => opt.Condition(src => (src.RawMaterialId != null)))
                .ForMember(dest => dest.FormulaText, opt => opt.Condition(src => (src.FormulaText != null)))
                .ForMember(dest => dest.FormulaTypeId, opt => opt.Condition(src => (src.FormulaTypeId != null)))
                .ForMember(dest => dest.ModelId, opt => opt.Condition(src => (src.ModelId != null)))
                .ForMember(dest => dest.UsageAmount, opt => opt.Condition(src => (src.UsageAmount != null)));


        }

    }
}

