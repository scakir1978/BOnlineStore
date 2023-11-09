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
            /// <summary>
            /// Montaj
            /// </summary>
            Assembly,
            /// <summary>
            /// Kargo
            /// </summary>
            Cargo,
            /// <summary>
            /// Fabrika Teslim
            /// </summary>
            FactoryFinished,
            /// <summary>
            /// Bayi Teslim
            /// </summary>
            DealerDelivery,
        }
    }
}
