using AutoMapper;
using BOnlineStore.IdentityServer.Business.UserService;
using BOnlineStore.IdentityServer.Data;
using BOnlineStore.IdentityServer.Dtos;
using BOnlineStore.IdentityServer.Models;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BOnlineStore.IdentityServer.Business.TenantService
{
    /// <summary>
    /// Tenant işlemlerini yöneten servis. Oluşturma, güncelleme, silme ve sorgulama işlemlerini gerçekleştirir.
    /// </summary>
    public class TenantManager : ITenantService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IUserService _userManager;

        public TenantManager(ApplicationDbContext context, IMapper mapper, IStringLocalizer<Language> stringLocalizer, IUserService userManager)
        {
            _context = context;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        /// <summary>
        /// Yeni tenant oluşturur.
        /// </summary>
        /// <param name="tenantDto">Oluşturulacak tenant bilgileri</param>
        /// <returns>Oluşturulan tenant ve işlem sonucu</returns>
        public async Task<Response<TenantDto>> CreateAsync(TenantCreateDto tenantDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Tenant zaten var mı kontrol et
                var existingTenant = await _context.Tenant
                    .FirstOrDefaultAsync(x => x.Name == tenantDto.Name);
                    
                if (existingTenant != null)
                {
                    return Response<TenantDto>.Fail(new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.TenantAlreadyExists),
                        Message = _stringLocalizer[IdentityServerKeys.TenantAlreadyExists]
                    }, HttpStatusCode.BadRequest);
                }

                var tenant = _mapper.Map<Tenant>(tenantDto);
                var result = await _context.Tenant.AddAsync(tenant);
                await _context.SaveChangesAsync();

                var createdTenantDto = _mapper.Map<TenantDto>(result.Entity);

                // Varsayılan admin kullanıcısını oluştur
                var resultUser = await _userManager.CreateDefaultUserAsync(tenant.Id, tenantDto.AdminUserEmail, tenantDto.AdminUserPassword);
                if (!resultUser.IsSucceed)
                {
                    await transaction.RollbackAsync();
                    return Response<TenantDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.CreateDefaultAdminUserError),
                            Message = _stringLocalizer[IdentityServerKeys.CreateDefaultAdminUserError]
                        }, HttpStatusCode.InternalServerError);
                }

                await transaction.CommitAsync();
                return Response<TenantDto>.Success(createdTenantDto, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Response<TenantDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.CreateTenantError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.CreateTenantError], ex.Message),
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Var olan tenant'ı günceller.
        /// </summary>
        /// <param name="id">Güncellenecek tenant kimliği</param>
        /// <param name="tenantDto">Güncellenecek tenant bilgileri</param>
        /// <returns>Güncellenen tenant ve işlem sonucu</returns>
        public async Task<Response<TenantDto>> UpdateAsync(Guid id, TenantUpdateDto tenantDto)
        {
            try
            {
                var existingTenant = await _context.Tenant
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (existingTenant == null)
                {
                    return Response<TenantDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.TenantNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.TenantNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Map the update values to the existing tracked entity
                _mapper.Map(tenantDto, existingTenant);
                existingTenant.UpdateDateTime = DateTime.Now;

                await _context.SaveChangesAsync();

                var updatedTenantDto = _mapper.Map<TenantDto>(existingTenant);
                return Response<TenantDto>.Success(updatedTenantDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<TenantDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.UpdateTenantError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.UpdateTenantError], ex.Message),
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tenant siler.
        /// </summary>
        /// <param name="id">Silinecek tenant kimliği</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<Response<TenantDto>> DeleteAsync(Guid id)
        {
            try
            {
                var existingTenant = await _context.Tenant
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (existingTenant == null)
                {
                    return Response<TenantDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.TenantNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.TenantNotFound]
                        }, HttpStatusCode.NotFound);
                }

                // Tenant'ı silmeden önce DTO'ya dönüştür
                var tenantDto = _mapper.Map<TenantDto>(existingTenant);

                _context.Tenant.Remove(existingTenant);
                await _context.SaveChangesAsync();

                return Response<TenantDto>.Success(tenantDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<TenantDto>.Fail(
                    new Error
                    {
                        ErrorCode = nameof(IdentityServerKeys.DeleteTenantError),
                        Message = string.Format(_stringLocalizer[IdentityServerKeys.DeleteTenantError], ex.Message),
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tenant'ı ID ile getirir.
        /// </summary>
        /// <param name="id">Tenant kimliği</param>
        /// <returns>Tenant bilgileri</returns>
        public async Task<Response<TenantDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var tenant = await _context.Tenant
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tenant == null)
                {
                    return Response<TenantDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.TenantNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.TenantNotFound]
                        }, HttpStatusCode.NotFound);
                }

                var tenantDto = _mapper.Map<TenantDto>(tenant);
                return Response<TenantDto>.Success(tenantDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<TenantDto>.Fail(
                    new Error
                    {
                        ErrorCode = "GetTenantByIdError",
                        Message = $"Error getting tenant by ID: {ex.Message}",
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tenant'ı adıyla getirir.
        /// </summary>
        /// <param name="name">Tenant adı</param>
        /// <returns>Tenant bilgileri</returns>
        public async Task<Response<TenantDto>> GetByNameAsync(string name)
        {
            try
            {
                var tenant = await _context.Tenant
                    .FirstOrDefaultAsync(x => x.Name == name);

                if (tenant == null)
                {
                    return Response<TenantDto>.Fail(
                        new Error
                        {
                            ErrorCode = nameof(IdentityServerKeys.TenantNotFound),
                            Message = _stringLocalizer[IdentityServerKeys.TenantNotFound]
                        }, HttpStatusCode.NotFound);
                }

                var tenantDto = _mapper.Map<TenantDto>(tenant);
                return Response<TenantDto>.Success(tenantDto, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<TenantDto>.Fail(
                    new Error
                    {
                        ErrorCode = "GetTenantByNameError",
                        Message = $"Error getting tenant by name: {ex.Message}",
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tüm tenant'ları listeler.
        /// </summary>
        /// <returns>Tenant listesi</returns>
        public async Task<Response<List<TenantDto>>> GetAllAsync()
        {
            try
            {
                var tenants = await _context.Tenant.ToListAsync();
                var tenantDtos = _mapper.Map<List<TenantDto>>(tenants);
                return Response<List<TenantDto>>.Success(tenantDtos, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<List<TenantDto>>.Fail(
                    new Error
                    {
                        ErrorCode = "GetAllTenantsError",
                        Message = $"Error getting all tenants: {ex.Message}",
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Herhangi bir tenant'ın var olup olmadığını kontrol eder.
        /// </summary>
        /// <returns>Tenant varlık kontrolü sonucu</returns>
        public async Task<Response<bool>> IsAnyTenantExistAsync()
        {
            try
            {
                var exists = await _context.Tenant.AnyAsync();
                return Response<bool>.Success(exists, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(
                    new Error
                    {
                        ErrorCode = "CheckTenantExistenceError",
                        Message = $"Error checking tenant existence: {ex.Message}",
                        StackTrace = ex.StackTrace
                    }, HttpStatusCode.InternalServerError);
            }
        }
    }
}
