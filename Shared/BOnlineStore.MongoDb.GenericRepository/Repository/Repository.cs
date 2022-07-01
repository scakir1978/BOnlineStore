using BOnlineStore.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IContext _context;
        protected readonly IMongoCollection<TEntity> _entity;

        protected Repository(IContext context)
        {            
            _context = context;
            this._entity = _context.GetCollection<TEntity>(typeof(TEntity).Name);            
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                ? _entity.AsQueryable()
                : _entity.AsQueryable().Where(predicate);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            return _entity.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await _entity.InsertOneAsync(entity, options);
            return entity;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await _entity.BulkWriteAsync((IEnumerable<WriteModel<TEntity>>)entities, options)).IsAcknowledged;
        }

        public virtual async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            return await _entity.FindOneAndReplaceAsync(x => x.Id == id, entity);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.FindOneAndReplaceAsync(predicate, entity);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await _entity.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            return await _entity.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.FindOneAndDeleteAsync(filter);
        }

        public virtual async Task<IClientSessionHandle> StartSession()
        {
            return await _context.Client.StartSessionAsync();
        }
    }
}
