namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// İş emri formu çıktısı için oluşturulacak dto
    /// </summary>
    public class WorkOrderFormDto
    {
        /// <summary>
        /// İş emri bilgileri
        /// </summary>
        public WorkOrderDto? WorkOrder { get; set; }

        /// <summary>
        /// Üretilecek ürünlerin bilgilerinin bulunduğu dto
        /// </summary>
        public List<WorkOrderProductionListDto>? WorkOrderProductionList { get; set; }

        public WorkOrderFormDto()
        { }

        public WorkOrderFormDto(WorkOrderDto? workOrder, List<WorkOrderProductionListDto>? workOrderProductionList)
        {
            WorkOrder = workOrder;
            WorkOrderProductionList = workOrderProductionList;
        }


    }
}
