using BOnlineStore.Shared.Entities;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;

namespace BOnlineStore.MongoDb.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Load(Expression<Func<TEntity, bool>>? predicate = null);
        Task<List<TEntity>> GetAsync();        
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(Guid id, TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> DeleteAsync(Guid id);        
        Task<IClientSessionHandle> StartSession();        
    }
}
