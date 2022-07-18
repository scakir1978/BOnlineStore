using BOnlineStore.Shared.Entities;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Linq.Expressions;
using BOnlineStore.Shared.Constansts;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IContext _context;
        protected readonly IMongoCollection<TEntity> _entity;

        protected Repository(IContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._entity = _context.GetCollection<TEntity>(typeof(TEntity).Name);
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual IQueryable<TEntity> Load(Expression<Func<TEntity, bool>>? predicate = null)
        {
            var query = _entity.AsQueryable().Where(x => x.TenantId == GetTenantId());

            return predicate == null
                ? query
                : query.Where(predicate);
        }

        public virtual async Task<List<TEntity>> GetAsync()
        {
            return await _entity.Find(x=>x.TenantId == GetTenantId()).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _entity.Find(x => x.TenantId == GetTenantId() && x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            entity.SetTenant(GetTenantId());
            await _entity.InsertOneAsync(entity, options);
            return entity;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };

            foreach (var entity in entities)
            {
                entity.SetTenant(GetTenantId());
            }

            return (await _entity.BulkWriteAsync((IEnumerable<WriteModel<TEntity>>)entities, options)).IsAcknowledged;
        }

        public virtual async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {            
            var options = new FindOneAndReplaceOptions<TEntity> { ReturnDocument = ReturnDocument.After };
            entity.SetTenant(GetTenantId());
            return await _entity.FindOneAndReplaceAsync<TEntity>(x => x.TenantId == GetTenantId() && x.Id == id, entity, options );
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var options = new FindOneAndReplaceOptions<TEntity> { ReturnDocument = ReturnDocument.After };
            entity.SetTenant(GetTenantId());
            return await _entity.FindOneAndReplaceAsync(predicate, entity, options);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await _entity.FindOneAndDeleteAsync(x => x.TenantId == GetTenantId() && x.Id == entity.Id);
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            return await _entity.FindOneAndDeleteAsync(x => x.TenantId == GetTenantId() && x.Id == id);
        }

        public virtual async Task<IClientSessionHandle> StartSession()
        {
            return await _context.Client.StartSessionAsync();
        }

        private Guid GetTenantId()
        {
            var tenantId = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == GlobalConstants.tenantId)?.Value ?? "";
            return new Guid(tenantId);
        }
    }
}
