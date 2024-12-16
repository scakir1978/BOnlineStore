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

            if (typeInterface == null || typeClass == null)
            {
                throw new ArgumentException("Invalid entity name provided.");
            }

            //Interface tipine bağlı DI sisteminden servise ulaşılır.
            var service = _serviceProvider.GetService(typeInterface);

            if (service == null)
            {
                throw new InvalidOperationException($"Service for type {typeInterface.FullName} not found.");
            }

            //Class tipinden çağıracağımız GetByIdAsync metoduna ulaşılır.
            var method = typeClass.GetMethod("GetByIdAsync");

            if (method == null)
            {
                throw new InvalidOperationException($"Method GetByIdAsync not found in type {typeClass.FullName}.");
            }

            //Method service kullanılarak, gönderilen entity id ile çağrılır.
            var result = method.Invoke(service, new object[] { entityId });

            if (result == null)
            {
                throw new InvalidOperationException("The invoked method returned null.");
            }

            //Dönen metod async bir fonksiyon olduğu için await ile sonucuna ulaşılır
            await (dynamic)result;

            //entity datası oluşturulur.
            var entity = ((dynamic)result).GetAwaiter().GetResult();

            return (object)entity;
        }
    }
}
