using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using BOnlineStore.Services.Production.Api.Repositories;
using BOnlineStore.Shared;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Production.Api.Services
{
    public class FormulaService : Service<Formula, FormulaDto, FormulaCreateDto, FormulaUpdateDto>, IFormulaService
    {
        private readonly IFormulaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public FormulaService(
            IFormulaRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<Formula> validator) : base(repository, mapper, stringLocalizer, validator)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Seçilen formülü, yine seçilen modele kopyalar.
        /// </summary>
        /// <param name="formulaId">Kopyalanacak formülün idsi</param>
        /// <param name="modelId">Formülün kopyalanacağı modelin idsi</param>        
        public async Task<bool> CopyFormula(string formulaId, string modelId)
        {
            if (string.IsNullOrWhiteSpace(formulaId))
            {
                throw new Exception(_stringLocalizer[ProductionApiKeys.FormulaIdNotEmpty]);
            }

            if (string.IsNullOrWhiteSpace(modelId))
            {
                throw new Exception(_stringLocalizer[ProductionApiKeys.ModelIdNotEmpty]);
            }

            var formula = await _repository.GetByIdAsync(formulaId);

            if (formula == null)
            {
                throw new InvalidOperationException(_stringLocalizer[ProductionApiKeys.FormulaIdNotFound]);
            }

            var formulaCreateDto = _mapper.Map<FormulaCreateDto>(formula);
            formulaCreateDto.ModelId = modelId;
            formulaCreateDto.Id = formula.GetNewId();
            await AddAsync(formulaCreateDto);

            return true;

        }

        /// <summary>
        /// Formüş girişi ekranında tanımlanan formülün, 
        /// düzgün oluşup oluşmadığını anlamak için, sanal değerler ile formül oluşturulup çalıştırılır.
        /// Eğer formül düzgün oluşmuş ise formül sanal değerler ile hata vermeden çalışır.
        /// Formülün içinde EN1, EN2, EN3, YUKSEKLIK ve SONUCDEGISKENI için "100" değeri kullanılır. 
        /// SABIT içinse girilen değer kullanılır.        
        /// </summary>
        /// <param name="formulaDetails"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteFormulaTest(List<FormulaDetail> formulaDetails)
        {
            try
            {
                string formulaText = string.Empty;

                foreach (FormulaDetail formulaDetail in formulaDetails)
                {
                    switch (formulaDetail.VariableType)
                    {
                        case FormulaVariableTypeConstants.EN1:
                        case FormulaVariableTypeConstants.EN2:
                        case FormulaVariableTypeConstants.EN3:
                        case FormulaVariableTypeConstants.YUKSEKLIK:
                        case FormulaVariableTypeConstants.SONUCDEGISKENI:
                            formulaText = formulaText + " 100";
                            break;
                        case FormulaVariableTypeConstants.SABIT:
                            formulaText = formulaText + " " + formulaDetail.VariableValue?.ToString() ?? "";
                            break;
                        case FormulaVariableTypeConstants.PARANTEZAC:
                            formulaText = formulaText + " (";
                            break;
                        case FormulaVariableTypeConstants.PARANTEZKAPA:
                            formulaText = formulaText + " )";
                            break;
                        case FormulaVariableTypeConstants.ARTI:
                            formulaText = formulaText + " +";
                            break;
                        case FormulaVariableTypeConstants.EKSI:
                            formulaText = formulaText + " -";
                            break;
                        case FormulaVariableTypeConstants.CARPI:
                            formulaText = formulaText + " *";
                            break;
                        case FormulaVariableTypeConstants.BOLU:
                            formulaText = formulaText + " /";
                            break;
                    }
                }

                NCalc.Expression e = new NCalc.Expression(formulaText.Trim());

                e.Evaluate();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
