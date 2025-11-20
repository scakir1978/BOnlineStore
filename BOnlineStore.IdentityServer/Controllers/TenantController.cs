using Microsoft.AspNetCore.Mvc;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Dtos;
using Microsoft.AspNetCore.Authorization;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using Microsoft.Extensions.Localization;
using BOnlineStore.Shared.Controllers;
using DevExtreme.AspNet.Data;
using static Duende.IdentityServer.IdentityServerConstants;

namespace BOnlineStore.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class TenantController : ControllerShared
    {
        private readonly ITenantService _tenantService;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public TenantController(ITenantService tenantService, IStringLocalizer<Language> stringLocalizer)
        {
            _tenantService = tenantService;
            _stringLocalizer = stringLocalizer;
        }

        [HttpPost("Load")]
        public async Task<IActionResult> Load(DataSourceLoadOptionsBase loadOptions)
        {
            loadOptions.StringToLower = true;
            var response = await _tenantService.GetAllAsync();
            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }
            return CreateSuccessActionResultInstance(DataSourceLoader.Load(response.Result, loadOptions));
        }

        /// <summary>
        /// Tüm firmaları getirir
        /// </summary>
        /// <returns>Firma listesi</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _tenantService.GetAllAsync();
            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }
            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        /// <summary>
        /// ID'ye göre firma getirir
        /// </summary>
        /// <param name="id">Firma kimliği</param>
        /// <returns>Firma bilgileri</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _tenantService.GetByIdAsync(id);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        /// <summary>
        /// İsme göre firma getirir
        /// </summary>
        /// <param name="name">Firma adı</param>
        /// <returns>Firma bilgileri</returns>
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var response = await _tenantService.GetByNameAsync(name);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        /// <summary>
        /// Yeni firma oluşturur
        /// </summary>
        /// <param name="tenantCreateDto">Oluşturulacak firma bilgileri</param>
        /// <returns>Oluşturulan firma bilgileri</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TenantCreateDto tenantCreateDto)
        {
            var response = await _tenantService.CreateAsync(tenantCreateDto);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance<TenantDto>(null, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        /// <summary>
        /// Firma bilgilerini günceller
        /// </summary>
        /// <param name="id">Güncellenecek firma kimliği</param>
        /// <param name="tenantUpdateDto">Güncellenecek firma bilgileri</param>
        /// <returns>Güncellenmiş firma bilgileri</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TenantUpdateDto tenantUpdateDto)
        {
            if (id != tenantUpdateDto.Id)
            {
                var error = new BOnlineStore.Shared.Dtos.Error 
                { 
                    ErrorCode = "TENANT_ID_MISMATCH", 
                    Message = _stringLocalizer[IdentityServerKeys.TenantIdMismatch] 
                };
                return CreateErrorActionResultInstance<TenantDto>(null, new List<BOnlineStore.Shared.Dtos.Error> { error });
            }

            var response = await _tenantService.UpdateAsync(id, tenantUpdateDto);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        /// <summary>
        /// Firma siler
        /// </summary>
        /// <param name="id">Silinecek firma kimliği</param>
        /// <returns>Silme işlemi sonucu</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _tenantService.DeleteAsync(id);

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }

        /// <summary>
        /// Sistemde herhangi bir firma olup olmadığını kontrol eder
        /// </summary>
        /// <returns>Firma varlık durumu</returns>
        [HttpGet("exists")]
        public async Task<IActionResult> IsAnyTenantExist()
        {
            var response = await _tenantService.IsAnyTenantExistAsync();

            if (!response.IsSucceed)
            {
                return CreateErrorActionResultInstance(response.Result, response.Errors);
            }

            return CreateActionResultInstance(response.Result, response.StatusCode);
        }
    }
}
