using BOnlineStore.Shared;
using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Reçete türü
    /// </summary>
    public class RecipeType : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// Reçete türünün hangi form çıktısı ile çalıştığını belirtir.
        /// </summary>
        public FormNameEnum.FormName? FormName { get; private set; }

        /// <summary>
        /// Değer true ise bu imalat reçetesinde panel var demektir.
        /// </summary>
        public bool? ThisRecipeHasPanel { get; private set; }
        /// <summary>
        /// Reçeteye ait hammadde listesi
        /// </summary>
        public List<RawMaterialId>? RawMaterialIds { get; private set; }
        /// <summary>
        /// Reçetede panel varsa ona ait hammadde listesi
        /// </summary>
        public List<RawMaterialId>? PanelRawMaterialIds { get; private set; }
        /// <summary>
        /// Cam boylarında kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialId>? GlassLengthRawMaterialIds { get; private set; }
        /// <summary>
        /// Cam eninde kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialId>? GlassWidthRawMaterialIds { get; private set; }
        /// <summary>
        /// Reçetede panel varsa, panelin cam boyunda kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialId>? PanelGlassLengthRawMaterialIds { get; private set; }
        /// <summary>
        /// Reçetede panel varsa, panelin cam eninde kullanılacak hammadde listesi
        /// </summary>
        public List<RawMaterialId>? PanelGlassWidthRawMaterialIds { get; private set; }


        public RecipeType() : base()
        {
            Code = "";
            Name = "";
        }

        public RecipeType(
            Guid tenantId, string id, string code, string name, FormNameEnum.FormName? formName = null, bool? thisRecipeHasPanel = null,
            List<RawMaterialId>? rawMaterialIds = null, List<RawMaterialId>? panelRawMaterialIds = null,
            List<RawMaterialId>? glassLengthRawMaterialIds = null, List<RawMaterialId>? glassWidthRawMaterialIds = null,
            List<RawMaterialId>? panelGlassLengthRawMaterialIds = null, List<RawMaterialId>? panelGlassWidthRawMaterialIds = null) : base(tenantId, id)
        {
            Code = code;
            Name = name;
            FormName = formName;
            ThisRecipeHasPanel = thisRecipeHasPanel;
            RawMaterialIds = rawMaterialIds;
            PanelRawMaterialIds = panelRawMaterialIds;
            GlassLengthRawMaterialIds = glassLengthRawMaterialIds;
            GlassWidthRawMaterialIds = glassWidthRawMaterialIds;
            PanelGlassLengthRawMaterialIds = panelGlassLengthRawMaterialIds;
            PanelGlassWidthRawMaterialIds = panelGlassWidthRawMaterialIds;

        }

        public void UpdateRecipeType(
            string code,
            string name,
            FormNameEnum.FormName? formName = null,
            bool? thisRecipeHasPanel = null,
            List<RawMaterialId>? rawMaterialIds = null,
            List<RawMaterialId>? panelRawMaterialIds = null,
            List<RawMaterialId>? glassLengthRawMaterialIds = null,
            List<RawMaterialId>? glassWidthRawMaterialIds = null,
            List<RawMaterialId>? panelGlassLengthRawMaterialIds = null,
            List<RawMaterialId>? panelGlassWidthRawMaterialIds = null)
        {
            Code = code;
            Name = name;
            FormName = formName;
            ThisRecipeHasPanel = thisRecipeHasPanel;
            RawMaterialIds = rawMaterialIds;
            PanelRawMaterialIds = panelRawMaterialIds;
            GlassLengthRawMaterialIds = glassLengthRawMaterialIds;
            GlassWidthRawMaterialIds = glassWidthRawMaterialIds;
            PanelGlassLengthRawMaterialIds = panelGlassLengthRawMaterialIds;
            PanelGlassWidthRawMaterialIds = panelGlassWidthRawMaterialIds;
        }
    }

    public class RawMaterialId
    {
        public string? Id { get; set; }
    }
}
