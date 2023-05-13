using BOnlineStore.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    //Model tanımlama

    public class Model : Entity
    {
        /// <summary>
        /// Panel Kodu
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// Panel Açıklaması
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Model Grup Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModelGroupId { get; private set; }

        /// <summary>
        /// Yan Panel Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? SidePanelId { get; private set; }

        /// <summary>
        /// Panel Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PanelId { get; private set; }

        /// <summary>
        /// Reçete Türü Id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
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


        public Model() : base()
        {
            Code = "";
            Name = "";
        }

        public Model(
            Guid tenantId, string id, string code, string name, string? modelGroupId, string? sidePanelId = null,
            string? panelId = null, string? recipeTypeId = null, decimal? wastageRatio = null,
            decimal? wastageAmount = null, decimal? workmanshipRatio = null, decimal? workmanshipAmount = null,
            string? picture = null) : base(tenantId, id)
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

        public void UpdateModel(string code, string name, string? modelGroupId)
        {
            Code = code;
            Name = name;
            ModelGroupId = modelGroupId;
        }
    }
}
