using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public class Settings : ISettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
