using BOnlineStore.Shared;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RecipeTypeUpdateDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        /// <summary>
        /// Reçete türünün hangi form çıktısı ile çalıştığını belirtir.
        /// </summary>
        public FormNameEnum.FormName? FormName { get; set; }
        /// <summary>
        /// Değer true ise bu imalat reçetesinde panel var demektir.
        /// </summary>
        public bool? ThisRecipeHasPanel { get; set; }
        /// <summary>
        /// Reçeteye ait hammadde listesi
        /// </summary>
        public List<RawMaterialIdDto>? RawMaterialIds { get; set; }
        /// <summary>
        /// Reçetede panel varsa ona ait hammadde listesi
        /// </summary>
        public List<RawMaterialIdDto>? PanelRawMaterialIds { get; set; }
        /// <summary>
        /// Cam üretiminde  kullanılacak hammadde listesi
        /// </summary>
        public List<GlassRawMaterialIdDto>? GlassRawMaterialIds { get; set; }
        /// <summary>
        /// Reçetede panel varsa, panelin cam üretiminde kullanılacak hammadde listesi
        /// </summary>
        public List<GlassRawMaterialIdDto>? PanelGlassRawMaterialIds { get; set; }

        public RecipeTypeUpdateDto(
            string? code, string? name, FormNameEnum.FormName? formName = null, bool? thisRecipeHasPanel = null, List<RawMaterialIdDto>? rawMaterialIds = null,
            List<RawMaterialIdDto>? panelRawMaterialIds = null, List<GlassRawMaterialIdDto>? glassRawMaterialIds = null,
            List<GlassRawMaterialIdDto>? panelGlassRawMaterialIds = null)
        {
            Code = code;
            Name = name;
            FormName = formName;
            ThisRecipeHasPanel = thisRecipeHasPanel;
            RawMaterialIds = rawMaterialIds;
            PanelRawMaterialIds = panelRawMaterialIds;
            GlassRawMaterialIds = glassRawMaterialIds;
            PanelGlassRawMaterialIds = panelGlassRawMaterialIds;

        }
    }
}
