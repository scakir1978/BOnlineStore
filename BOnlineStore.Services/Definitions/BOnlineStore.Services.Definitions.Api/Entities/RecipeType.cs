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
        /// Cam üretiminde kullanılacak hammadde listesi
        /// </summary>
        public List<GlassRawMaterialId>? GlassRawMaterialIds { get; private set; }
        /// <summary>
        /// Reçetede panel varsa, panelin cam üretiminde kullanılacak hammadde listesi
        /// </summary>
        public List<GlassRawMaterialId>? PanelGlassRawMaterialIds { get; private set; }


        public RecipeType() : base()
        {
            Code = "";
            Name = "";
        }

        public RecipeType(
            Guid tenantId, string id, string code, string name, FormNameEnum.FormName? formName = null, bool? thisRecipeHasPanel = null,
            List<RawMaterialId>? rawMaterialIds = null, List<RawMaterialId>? panelRawMaterialIds = null,
            List<GlassRawMaterialId>? glassRawMaterialIds = null, List<GlassRawMaterialId>? panelGlassRawMaterialIds = null) : base(tenantId, id)
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

        public void UpdateRecipeType(
            string code,
            string name,
            FormNameEnum.FormName? formName = null,
            bool? thisRecipeHasPanel = null,
            List<RawMaterialId>? rawMaterialIds = null,
            List<RawMaterialId>? panelRawMaterialIds = null,
            List<GlassRawMaterialId>? glassRawMaterialIds = null,
            List<GlassRawMaterialId>? panelGlassRawMaterialIds = null,
            List<RawMaterialId>? panelGlassWidthRawMaterialIds = null)
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

    /// <summary>
    /// Kullanılan hammaddenin id bilgisi
    /// </summary>
    public class RawMaterialId
    {
        public string? Id { get; set; }
    }

    /// <summary>
    /// Modelin cam üretiminde kullanılan en ve boy hammadde bilgileri
    /// </summary>
    public class GlassRawMaterialId
    {
        /// <summary>
        /// Camın en bilgisi için kullanılan hammadder idsi
        /// </summary>
        public string? WidthMaterialId { get; set; }

        /// <summary>
        /// Camın boy bilgisi için kullanılan hammadder idsi
        /// </summary>
        public string? LengthMaterialId { get; set; }
    }
}
