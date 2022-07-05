using BOnlineStore.Shared.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(Guid id, TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> DeleteAsync(Guid id);
        Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IClientSessionHandle> StartSession();        
    }
}
