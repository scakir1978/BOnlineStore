using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Shared.Entities;
using MongoDB.Bson;

namespace BOnlineStore.Services.Definitions.Api.Entities
{
    /// <summary>
    /// Ölçü/Montaj Limitleri
    /// </summary>
    public class MeasurementAssemblyLimits : Entity
    {
        /// <summary>
        /// Gün
        /// </summary>
        public int? Day { get; private set; }
        /// <summary>
        /// Montör
        /// </summary>
        public string? AssemblerId { get; private set; }
        /// <summary>
        /// Bölge-01
        /// </summary>
        public string? Region01Id { get; private set; }
        /// <summary>
        /// Bölge-02
        /// </summary>
        public string? Region02Id { get; private set; }
        /// <summary>
        /// Bölge-03
        /// </summary>
        public string? Region03Id { get; private set; }
        /// <summary>
        /// Bölge-04
        /// </summary>
        public string? Region04Id { get; private set; }
        /// <summary>
        /// Bölge-05
        /// </summary>
        public string? Region05Id { get; private set; }
        /// <summary>
        /// Ölçü Adedi
        /// </summary>
        public int? MeasurementQuantity { get; private set; }
        /// <summary>
        /// Montaj Adedi
        /// </summary>
        public int? AssemblyQuantity { get; private set; }

        public MeasurementAssemblyLimits() : base()
        {
            Day = 0;
            AssemblerId = "";
            Region01Id = "";
            Region02Id = "";
            Region03Id = "";
            Region04Id = "";
            Region05Id = "";
            MeasurementQuantity = 0;
            AssemblyQuantity = 0;
        }

        public MeasurementAssemblyLimits(
            Guid tenantId, string id, int? day = null, string? assemblerId = null,
            string? region01Id = null, string? region02Id = null, string? region03Id = null, string? region04Id = null,
            string? region05Id = null, int? measurementQuantity = null, int? assemblyQuantity = null) : base(tenantId, id)
        {
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

        public void UpdateMeasurementAssemblyLimits(
            int? day = null, string? assemblerId = null, string? region01Id = null,
            string? region02Id = null, string? region03Id = null, string? region04Id = null, string? region05Id = null,
            int? measurementQuantity = null, int? assemblyQuantity = null)
        {
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
