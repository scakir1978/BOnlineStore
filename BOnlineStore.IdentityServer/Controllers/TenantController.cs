using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BOnlineStore.IdentityServer.Business.TenantService;
using BOnlineStore.IdentityServer.Dtos;
using Microsoft.AspNetCore.Authorization;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public TenantController(ITenantService tenantService, IStringLocalizer<Language> stringLocalizer)
        {
            _tenantService = tenantService;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Tüm firmaları getirir
        /// </summary>
        /// <returns>Firma listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDto>>> GetAllTenants()
        {
            var response = await _tenantService.GetAllAsync();
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// ID'ye göre firma getirir
        /// </summary>
        /// <param name="id">Firma kimliği</param>
        /// <returns>Firma bilgileri</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TenantDto>> GetTenantById(Guid id)
        {
            var response = await _tenantService.GetByIdAsync(id);
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// İsme göre firma getirir
        /// </summary>
        /// <param name="name">Firma adı</param>
        /// <returns>Firma bilgileri</returns>
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<TenantDto>> GetTenantByName(string name)
        {
            var response = await _tenantService.GetByNameAsync(name);
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Yeni firma oluşturur
        /// </summary>
        /// <param name="tenantCreateDto">Oluşturulacak firma bilgileri</param>
        /// <returns>Oluşturulan firma bilgileri</returns>
        [HttpPost]
        public async Task<ActionResult<TenantDto>> CreateTenant([FromBody] TenantCreateDto tenantCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tenantService.CreateAsync(tenantCreateDto);
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return CreatedAtAction(nameof(GetTenantById), new { id = response.Result.Id }, response.Result);
        }

        /// <summary>
        /// Firma bilgilerini günceller
        /// </summary>
        /// <param name="id">Güncellenecek firma kimliği</param>
        /// <param name="tenantUpdateDto">Güncellenecek firma bilgileri</param>
        /// <returns>Güncellenmiş firma bilgileri</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TenantDto>> UpdateTenant(Guid id, [FromBody] TenantUpdateDto tenantUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tenantUpdateDto.Id)
            {
                return BadRequest(_stringLocalizer[IdentityServerKeys.TenantIdMismatch]);
            }

            var response = await _tenantService.UpdateAsync(id, tenantUpdateDto);
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Firma siler
        /// </summary>
        /// <param name="id">Silinecek firma kimliği</param>
        /// <returns>Silme işlemi sonucu</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTenant(Guid id)
        {
            var response = await _tenantService.DeleteAsync(id);
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return NoContent();
        }

        /// <summary>
        /// Sistemde herhangi bir firma olup olmadığını kontrol eder
        /// </summary>
        /// <returns>Firma varlık durumu</returns>
        [HttpGet("exists")]
        public async Task<ActionResult<bool>> IsAnyTenantExist()
        {
            var response = await _tenantService.IsAnyTenantExistAsync();
            if (!response.IsSucceed)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return Ok(response.Result);
        }
    }
}
