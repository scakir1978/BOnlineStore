using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelCreateDto : EntityDto
    {
        /// <summary>
        /// Model Kodu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Model Açıklaması
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Model Grup Id
        /// </summary>
        public string ModelGroupId { get; set; }

        /// <summary>
        /// Yan Panel Id
        /// </summary>        
        public string? SidePanelId { get; private set; }

        /// <summary>
        /// Panel Id
        /// </summary>
        public string? PanelId { get; private set; }

        /// <summary>
        /// Reçete Türü Id
        /// </summary>
        public string? RecipeTypeId { get; private set; }

        /// <summary>
        /// Yüzde Fire Oranı 
        /// </summary>
        public decimal? WastageRatio { get; private set; }

        /// <summary>
        /// Fire Tutarı
        /// </summary>
        public decimal? WastageAmount { get; private set; }

        /// <summary>
        /// Yüzde İşçilik Oranı 
        /// </summary>
        public decimal? WorkmanshipRatio { get; private set; }

        /// <summary>
        /// İşçilik Tutarı
        /// </summary>
        public decimal? WorkmanshipAmount { get; private set; }

        /// <summary>
        /// Base64 formatında resim
        /// </summary>
        public string? Picture { get; private set; }

        public ModelCreateDto(
            string code, string name, string modelGroupId, string? sidePanelId = null, string? panelId = null,
            string? recipeTypeId = null, decimal? wastageRatio = null, decimal? wastageAmount = null,
            decimal? workmanshipRatio = null, decimal? workmanshipAmount = null, string? picture = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
            SidePanelId = sidePanelId;
            PanelId = panelId;
            RecipeTypeId = recipeTypeId;
            WastageRatio = wastageRatio;
            WastageAmount = wastageAmount;
            WorkmanshipRatio = workmanshipRatio;
            WorkmanshipAmount = workmanshipAmount;
            Picture = picture;
        }
    }
}
