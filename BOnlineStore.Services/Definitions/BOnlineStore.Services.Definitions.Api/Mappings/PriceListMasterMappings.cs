using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class PriceListMasterMappings : Profile
    {
        public PriceListMasterMappings()
        {
            CreateMap<PriceListMaster, PriceListMasterDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<PriceListMaster, PriceListMasterLoadDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<PriceListMaster, PriceListMasterCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<PriceListMaster, PriceListMasterUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation()
                .ForMember(dest => dest.Code, opt => opt.Condition(src => (src.Code != null)))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != null)))
                .ForMember(dest => dest.EndDate, opt => opt.Condition(src => (src.EndDate != null)))
                .ForMember(dest => dest.FirstDate, opt => opt.Condition(src => (src.FirstDate != null)))
                .ForMember(dest => dest.PriceListColorDifferences, opt => opt.Condition(src => (src.PriceListColorDifferences != null)))
                .ForMember(dest => dest.PriceListDetails, opt => opt.Condition(src => (src.PriceListDetails != null)))
                .ForMember(dest => dest.PriceListGlassDifferences, opt => opt.Condition(src => (src.PriceListGlassDifferences != null)))
                .ForMember(dest => dest.PriceListMeasurementDifferences, opt => opt.Condition(src => (src.PriceListMeasurementDifferences != null)));


            CreateMap<PriceListColorDifference, PriceListColorDifferenceDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<PriceListDetail, PriceListDetailDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<PriceListGlassDifference, PriceListGlassDifferenceDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<PriceListMeasurementDifference, PriceListMeasurementDifferenceDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();

        }

    }
}

