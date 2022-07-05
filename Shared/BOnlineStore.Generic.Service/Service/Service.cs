using AutoMapper;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Generic.Service
{
    public abstract class Service<TEntity, TEntityDto, TCreateInput, TUpdateInput> : IService<TEntity, TEntityDto, TCreateInput, TUpdateInput> where TEntity : IEntity
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TEntityDto> AddAsync(TCreateInput input)
        {
            return _mapper.Map<TEntityDto>(await _repository.AddAsync(_mapper.Map<TEntity>(input)));            
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TCreateInput> inputs)
        {
            return await _repository.AddRangeAsync(_mapper.Map<List<TEntity>>(inputs));            
        }

        public async Task<TEntityDto> DeleteAsync(TEntityDto input)
        {
            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(_mapper.Map<TEntity>(input)));            
        }

        public async Task<TEntityDto> DeleteAsync(Guid id)
        {
            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(id));
        }

        public async Task<TEntityDto> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(predicate));
        }

        public IQueryable<TEntityDto> Get(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return _mapper.ProjectTo<TEntityDto>(_repository.Get(predicate));            
        }

        public async Task<TEntityDto> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _mapper.Map<TEntityDto>(await _repository.GetAsync(predicate));            
        }

        public async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<TEntityDto>(await _repository.GetByIdAsync(id));            
        }

        public async Task<TEntityDto> UpdateAsync(Guid id, TUpdateInput input)
        {
            TEntity updateEntity = await _repository.GetByIdAsync(id);
            if (updateEntity == null)
            {
                throw new Exception("Kayıt bulunamadı");
            }
            
            return _mapper.Map<TEntityDto>(await _repository.UpdateAsync(id, _mapper.Map(input, updateEntity)));
            
        }        

        public async Task<TEntityDto> UpdateAsync(TEntityDto input, Expression<Func<TEntity, bool>> predicate)
        {
            TEntity updateEntity = await _repository.GetAsync(predicate);
            if (updateEntity == null)
            {
                throw new Exception("Kayıt bulunamadı");
            }
            return _mapper.Map<TEntityDto>(await _repository.UpdateAsync(_mapper.Map(input, updateEntity), predicate));
        }
    }
}
