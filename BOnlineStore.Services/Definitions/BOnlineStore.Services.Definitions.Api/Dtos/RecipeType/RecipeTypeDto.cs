using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class RecipeTypeDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

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
        /// Cam boylarında kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialIdDto>? GlassLengthRawMaterialIds { get; set; }
        /// <summary>
        /// Cam eninde kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialIdDto>? GlassWidthRawMaterialIds { get; set; }
        /// <summary>
        /// Reçetede panel varsa, panelin cam boyunda kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialIdDto>? PanelGlassLengthRawMaterialIds { get; set; }
        /// <summary>
        /// Reçetede panel varsa, panelin cam eninde kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialIdDto>? PanelGlassWidthRawMaterialIds { get; set; }

        public RecipeTypeDto(
            string id,
            string code,
            string name,
            bool? thisRecipeHasPanel = null,
            List<RawMaterialIdDto>? rawMaterialIds = null,
            List<RawMaterialIdDto>? panelRawMaterialIds = null,
            List<RawMaterialIdDto>? glassLengthRawMaterialIds = null,
            List<RawMaterialIdDto>? glassWidthRawMaterialIds = null,
            List<RawMaterialIdDto>? panelGlassLengthRawMaterialIds = null,
            List<RawMaterialIdDto>? panelGlassWidthRawMaterialIds = null)
        {
            Id = id;
            Code = code;
            Name = name;
            ThisRecipeHasPanel = thisRecipeHasPanel;
            RawMaterialIds = rawMaterialIds;
            PanelRawMaterialIds = panelRawMaterialIds;
            GlassLengthRawMaterialIds = glassLengthRawMaterialIds;
            GlassWidthRawMaterialIds = glassWidthRawMaterialIds;
            PanelGlassLengthRawMaterialIds = panelGlassLengthRawMaterialIds;
            PanelGlassWidthRawMaterialIds = panelGlassWidthRawMaterialIds;
        }
    }
}
