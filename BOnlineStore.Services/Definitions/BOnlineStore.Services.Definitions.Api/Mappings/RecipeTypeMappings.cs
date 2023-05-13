using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;

namespace BOnlineStore.Services.Definitions.Api
{
    public class RecipeTypeMappings : Profile
    {
        public RecipeTypeMappings()
        {
            CreateMap<RecipeType, RecipeTypeDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RecipeType, RecipeTypeCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RecipeType, RecipeTypeUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation()
                .ForMember(dest => dest.Code, opt => opt.Condition(src => (src.Code != null)))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != null)))
                .ForMember(dest => dest.ThisRecipeHasPanel, opt => opt.Condition(src => (src.ThisRecipeHasPanel != null)))
                .ForMember(dest => dest.GlassLengthRawMaterialIds, opt => opt.Condition(src => (src.GlassLengthRawMaterialIds != null)))
                .ForMember(dest => dest.GlassWidthRawMaterialIds, opt => opt.Condition(src => (src.GlassWidthRawMaterialIds != null)))
                .ForMember(dest => dest.PanelGlassLengthRawMaterialIds, opt => opt.Condition(src => (src.PanelGlassLengthRawMaterialIds != null)))
                .ForMember(dest => dest.PanelGlassWidthRawMaterialIds, opt => opt.Condition(src => (src.PanelGlassWidthRawMaterialIds != null)))
                .ForMember(dest => dest.RawMaterialIds, opt => opt.Condition(src => (src.RawMaterialIds != null)))
                .ForMember(dest => dest.PanelRawMaterialIds, opt => opt.Condition(src => (src.PanelRawMaterialIds != null)));

            CreateMap<RawMaterialId, RawMaterialIdDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<RecipeType, RecipeTypeForComboDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();

        }

    }
}

