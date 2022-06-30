using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public interface ISettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
