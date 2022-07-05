using BOnlineStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Generic.Service
{
    public interface IService<TEntity, TEntityDto, in TCreateInput, in TUpdateInput> where TEntity : IEntity   
    {
        IQueryable<TEntityDto> Get(Expression<Func<TEntity, bool>>? predicate = null);
        Task<TEntityDto> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntityDto> GetByIdAsync(Guid id);
        Task<TEntityDto> AddAsync(TCreateInput input);
        Task<bool> AddRangeAsync(IEnumerable<TCreateInput> inputs);
        Task<TEntityDto> UpdateAsync(Guid id, TUpdateInput input);
        Task<TEntityDto> UpdateAsync(TEntityDto input, Expression<Func<TEntityDto, bool>> predicate);
        Task<TEntityDto> DeleteAsync(TEntityDto input);
        Task<TEntityDto> DeleteAsync(Guid id);
        Task<TEntityDto> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        
    }
}
