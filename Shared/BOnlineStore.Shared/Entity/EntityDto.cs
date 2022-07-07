using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Entity
{
    public  abstract class EntityDto
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
    }
}
