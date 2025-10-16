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
        public ActionResult<IEnumerable<TenantDto>> GetAllTenants()
        {
            try
            {
                var tenants = _tenantService.Tenants().ToList();
                return Ok(tenants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(_stringLocalizer[IdentityServerKeys.TenantsFetchError], ex.Message));
            }
        }

        /// <summary>
        /// ID'ye göre firma getirir
        /// </summary>
        /// <param name="id">Firma kimliği</param>
        /// <returns>Firma bilgileri</returns>
        [HttpGet("{id}")]
        public ActionResult<TenantDto> GetTenantById(Guid id)
        {
            try
            {
                var tenant = _tenantService.FindById(id);
                if (tenant == null)
                {
                    return NotFound(_stringLocalizer[IdentityServerKeys.TenantNotFound]);
                }
                return Ok(tenant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(_stringLocalizer[IdentityServerKeys.TenantFetchError], ex.Message));
            }
        }

        /// <summary>
        /// İsme göre firma getirir
        /// </summary>
        /// <param name="name">Firma adı</param>
        /// <returns>Firma bilgileri</returns>
        [HttpGet("by-name/{name}")]
        public ActionResult<TenantDto> GetTenantByName(string name)
        {
            try
            {
                var tenant = _tenantService.FindByName(name);
                if (tenant == null)
                {
                    return NotFound(_stringLocalizer[IdentityServerKeys.TenantNotFound]);
                }
                return Ok(tenant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(_stringLocalizer[IdentityServerKeys.TenantFetchError], ex.Message));
            }
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

            try
            {
                var createdTenant = await _tenantService.CreateAsync(tenantCreateDto);
                return CreatedAtAction(nameof(GetTenantById), new { id = createdTenant.Id }, createdTenant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

            try
            {
                var updatedTenant = await _tenantService.UpdateAsync(tenantUpdateDto);
                return Ok(updatedTenant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Firma siler
        /// </summary>
        /// <param name="id">Silinecek firma kimliği</param>
        /// <returns>Silme işlemi sonucu</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTenant(Guid id)
        {
            try
            {
                var result = await _tenantService.DeleteAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest(_stringLocalizer[IdentityServerKeys.TenantDeleteFailed]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Sistemde herhangi bir firma olup olmadığını kontrol eder
        /// </summary>
        /// <returns>Firma varlık durumu</returns>
        [HttpGet("exists")]
        public ActionResult<bool> IsAnyTenantExist()
        {
            try
            {
                var exists = _tenantService.IsAnyTenantExist();
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(_stringLocalizer[IdentityServerKeys.TenantExistenceCheckError], ex.Message));
            }
        }
    }
}
