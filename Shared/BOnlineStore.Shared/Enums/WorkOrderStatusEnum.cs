using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Enums
{
    public static class WorkOrderStatusEnum
    {
        public enum WorkOrderStatus
        {
            Assembly,
            Cargo,
            FactoryFinished,
            DealerDelivery,
        }
    }
}
