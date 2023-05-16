using BOnlineStore.Shared.Entity;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Dtos
{
    public class MeasurementAssemblyLimitsCreateDto : EntityDto
    {
        /// <summary>
        /// Gün
        /// </summary>
        public int? Day { get; set; }
        /// <summary>
        /// Montör
        /// </summary>
        public string? AssemblerId { get; set; }
        /// <summary>
        /// Bölge-01
        /// </summary>
        public string? Region01Id { get; set; }
        /// <summary>
        /// Bölge-02
        /// </summary>
        public string? Region02Id { get; set; }
        /// <summary>
        /// Bölge-03
        /// </summary>
        public string? Region03Id { get; set; }
        /// <summary>
        /// Bölge-04
        /// </summary>
        public string? Region04Id { get; set; }
        /// <summary>
        /// Bölge-05
        /// </summary>
        public string? Region05Id { get; set; }
        /// <summary>
        /// Ölçü Adedi
        /// </summary>
        public int? MeasurementQuantity { get; set; }
        /// <summary>
        /// Montaj Adedi
        /// </summary>
        public int? AssemblyQuantity { get; set; }

        public MeasurementAssemblyLimitsCreateDto(
            string code, string name, int? day = null, string? assemblerId = null, string? region01Id = null,
            string? region02Id = null, string? region03Id = null, string? region04Id = null, string? region05Id = null,
            int? measurementQuantity = null, int? assemblyQuantity = null)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Day = day;
            AssemblerId = assemblerId;
            Region01Id = region01Id;
            Region02Id = region02Id;
            Region03Id = region03Id;
            Region04Id = region04Id;
            Region05Id = region05Id;
            MeasurementQuantity = measurementQuantity;
            AssemblyQuantity = assemblyQuantity;
        }
    }
}
