using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Services.Definitions.Api.Helpers
{
    /// <summary>
    /// Microservislerdeki, DI ile kullanılarn servisleri entity ve foksiyon isimlerini string olarak vererek çalıştırır.    
    /// </summary>
    public static class RunDIServiceFunction
    {
        public static async Task<object> GetByIdAsync(IServiceProvider _serviceProvider, string entityName, string entityId)
        {
            //entity adına göre servisin interface tipi elde edilir.
            //Örneğin Region entitysi gönderilmesi durumunda IRegionService interface tipi elde edilir.
            var typeInterface = Type.GetType($"BOnlineStore.Services.Definitions.Api.Services.I{entityName.Trim()}Service");

            //entity adına göre servisin class tipi elde edilir.
            //Örneğin Region entitysi gönderilmesi durumunda RegionService class tipi elde edilir.
            var typeClass = Type.GetType($"BOnlineStore.Services.Definitions.Api.Services.{entityName.Trim()}Service");

            //Interface tipine bağlı DI sisteminden servise ulaşılır.
            var service = _serviceProvider.GetService(typeInterface);

            //Class tipinden çağıracağımız GetByIdAsync metoduna ulaşılır.
            var method = typeClass.GetMethod("GetByIdAsync");

            //Method service kullanılarak, gönderilen entity id ile çağrılır.
            dynamic result = method.Invoke(service, new object[] { entityId });

            //Dönen metod async bir fonksiyon olduğu için await ile sonucuna ulaşılır
            await result;

            //entity datası oluşturulur.
            var entity = result.GetAwaiter().GetResult();

            return (object)entity;
        }
    }
}
