using AutoMapper;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.MongoDb.GenericRepository;
using BOnlineStore.Shared.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
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
        public virtual async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new Exception("Kayıt Bulunamadı");

            return _mapper.Map<TEntityDto>(await _repository.GetByIdAsync(id));
        }
        public virtual async Task<TEntityDto> AddAsync(TCreateInput input)
        {
            #region Validation Control
            var validationResult = await ServiceValidator(ValidationTypeEnum.Create)
                                        .ValidateAsync(new ValidationContext<TCreateInput>(input));

            if (!validationResult.IsValid)
            {
                throw new ValidationException(_stringLocalizer[SharedKeys.ValidationErrors], validationResult.Errors);
            }
            #endregion

            var entity = _mapper.Map<TEntity>(input);

            var entityAdded = await _repository.AddAsync(entity);

            var entityDto = _mapper.Map<TEntityDto>(entityAdded);

            return entityDto;
        }
        public virtual async Task<bool> AddRangeAsync(IEnumerable<TCreateInput> inputs)
        {
            #region Validation Control
            foreach (var input in inputs)
            {
                var validationResult = await ServiceValidator(ValidationTypeEnum.Create).ValidateAsync(new ValidationContext<TCreateInput>(input));
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(_stringLocalizer[SharedKeys.ValidationErrors], validationResult.Errors);
                }
            }
            #endregion

            return await _repository.AddRangeAsync(_mapper.Map<List<TEntity>>(inputs));            
        }
        public virtual async Task<TEntityDto> DeleteAsync(TEntityDto input)
        {
            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(_mapper.Map<TEntity>(input)));            
        }
        public virtual async Task<TEntityDto> DeleteAsync(Guid id)
        {
            return _mapper.Map<TEntityDto>(await _repository.DeleteAsync(id));
        }        
        public virtual async Task<TEntityDto> UpdateAsync(Guid id, TUpdateInput input)
        {
            #region Validation Control
            var validationResult = await ServiceValidator(ValidationTypeEnum.Update)
                                        .ValidateAsync(new ValidationContext<TUpdateInput>(input));

            if (!validationResult.IsValid)
            {
                throw new ValidationException(_stringLocalizer[SharedKeys.ValidationErrors], validationResult.Errors);
            }
            #endregion

            #region Record Control            
            TEntity updateEntity = await _repository.GetByIdAsync(id);
            if (updateEntity == null)
            {
                throw new Exception("Kayıt bulunamadı");
            }
            #endregion

            return _mapper.Map<TEntityDto>(await _repository.UpdateAsync(id, _mapper.Map(input, updateEntity)));
            
        }        
        public virtual async Task<TEntityDto> UpdateAsync(TUpdateInput input, Expression<Func<TEntity, bool>> predicate)
        {
            #region Validation Control            
            var validationResult = await ServiceValidator(ValidationTypeEnum.Update)
                                        .ValidateAsync(new ValidationContext<TUpdateInput>(input));

            if (!validationResult.IsValid)
            {
                throw new ValidationException(_stringLocalizer[SharedKeys.ValidationErrors], validationResult.Errors);
            }
            #endregion

            #region Record Control            
            TEntity updateEntity = _repository.Load(predicate).First();
            if (updateEntity == null)
            {
                throw new Exception("Kayıt bulunamadı");
            }
            #endregion

            return _mapper.Map<TEntityDto>(await _repository.UpdateAsync(_mapper.Map(input, updateEntity), predicate));
        }
        public abstract IValidator ServiceValidator(ValidationTypeEnum validationType);

        
    }
}
