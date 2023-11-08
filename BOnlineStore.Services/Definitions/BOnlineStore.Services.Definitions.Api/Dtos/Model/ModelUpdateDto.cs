namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class ModelUpdateDto
    {
        /// <summary>
        /// Model Kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Model Açıklaması
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Model Grup Id
        /// </summary>
        public string? ModelGroupId { get; set; }

        /// <summary>
        /// Yan Panel Id
        /// </summary>
        public string? SidePanelId { get; set; }

        /// <summary>
        /// Panel Id
        /// </summary>
        public string? PanelId { get; set; }

        /// <summary>
        /// Reçete Türü Id
        /// </summary>
        public string? RecipeTypeId { get; set; }

        /// <summary>
        /// Yüzde Fire Oranı 
        /// </summary>
        public decimal? WastageRatio { get; set; }

        /// <summary>
        /// Fire Tutarı
        /// </summary>
        public decimal? WastageAmount { get; set; }

        /// <summary>
        /// Yüzde İşçilik Oranı 
        /// </summary>
        public decimal? WorkmanshipRatio { get; set; }

        /// <summary>
        /// İşçilik Tutarı
        /// </summary>
        public decimal? WorkmanshipAmount { get; set; }

        /// <summary>
        /// Base64 formatında resim
        /// </summary>
        public string? Picture { get; set; }

        public ModelUpdateDto(
            string? code, string? name, string? modelGroupId, string? sidePanelId = null, string? panelId = null,
            string? recipeTypeId = null, decimal? wastageRatio = null, decimal? wastageAmount = null,
            decimal? workmanshipRatio = null, decimal? workmanshipAmount = null, string? picture = null)
        {
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
