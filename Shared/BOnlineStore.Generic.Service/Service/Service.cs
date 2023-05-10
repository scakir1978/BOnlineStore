using AutoMapper;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Shared.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BOnlineStore.Shared.Enums;

namespace BOnlineStore.Generic.Service
{
    public abstract class Service<TEntity, TEntityDto, TCreateInput, TUpdateInput> : IService<TEntity, TEntityDto, TCreateInput, TUpdateInput> where TEntity : IEntity
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public Service(IRepository<TEntity> repository, IMapper mapper, IStringLocalizer<Language> stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public IQueryable<TEntity> Load(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return _repository.Load(predicate);
        }

        public virtual async Task<List<TEntityDto>> GetAsync()
        {
            return _mapper.Map<List<TEntityDto>>(await _repository.GetAsync());
        }

        public virtual async Task<TEntityDto> GetByIdAsync(string id)
        {
            await IsRecordExists(id);

            return _mapper.Map<TEntityDto>(await _repository.GetByIdAsync(id));
        }

        public virtual async Task<TEntityDto> AddAsync(TCreateInput input)
        {
            var entity = _mapper.Map<TEntity>(input);

            var entityAdded = await _repository.AddAsync(entity);

            var entityDto = _mapper.Map<TEntityDto>(entityAdded);

            return entityDto;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TCreateInput> inputs)
        {
            return await _repository.AddRangeAsync(_mapper.Map<List<TEntity>>(inputs));
        }

        public virtual async Task<TEntityDto> DeleteAsync(TEntityDto input)
        {
            var entity = _mapper.Map<TEntity>(input);

            await IsRecordExists(entity.Id);

            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(entity));
        }

        public virtual async Task<TEntityDto> DeleteAsync(string id)
        {
            await IsRecordExists(id);

            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(id));
        }

        public virtual async Task<TEntityDto> UpdateAsync(string id, TUpdateInput input)
        {
            #region Record Control            
            TEntity updateEntity = await _repository.GetByIdAsync(id);
            if (updateEntity == null)
            {
                throw new Exception(_stringLocalizer[SharedKeys.RecordNotFound, typeof(TEntity).Name, id]);
            }

            #endregion

            return _mapper.Map<TEntityDto>(await _repository.UpdateAsync(id, _mapper.Map(input, updateEntity)));

        }

        public virtual async Task<TEntityDto> UpdateAsync(TUpdateInput input, Expression<Func<TEntity, bool>> predicate)
        {

            #region Record Control            
            TEntity updateEntity = _repository.Load(predicate).First();
            if (updateEntity == null)
            {
                throw new Exception(_stringLocalizer[SharedKeys.RecordNotFoundPredicate, typeof(TEntity).Name]);
            }
            #endregion

            return _mapper.Map<TEntityDto>(await _repository.UpdateAsync(_mapper.Map(input, updateEntity), predicate));
        }

        private async Task IsRecordExists(string id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new Exception(_stringLocalizer[SharedKeys.RecordNotFound, typeof(TEntity).Name, id]);
        }


    }
}
