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
                        case FormulaVariableTypeConstants.WIDTH1:
                        case FormulaVariableTypeConstants.WIDTH2:
                        case FormulaVariableTypeConstants.WIDTH3:
                        case FormulaVariableTypeConstants.HEIGHT:
                        case FormulaVariableTypeConstants.RESULTVARIABLE:
                            formulaText = formulaText + " 100";
                            break;
                        case FormulaVariableTypeConstants.CONSTANT:
                            formulaText = formulaText + " " + formulaDetail.VariableValue?.ToString() ?? "";
                            break;
                        case FormulaVariableTypeConstants.OPENPARENTHESIS:
                            formulaText = formulaText + " (";
                            break;
                        case FormulaVariableTypeConstants.CLOSEPARENTHESIS:
                            formulaText = formulaText + " )";
                            break;
                        case FormulaVariableTypeConstants.PLUS:
                            formulaText = formulaText + " +";
                            break;
                        case FormulaVariableTypeConstants.MINUS:
                            formulaText = formulaText + " -";
                            break;
                        case FormulaVariableTypeConstants.MULTIPLY:
                            formulaText = formulaText + " *";
                            break;
                        case FormulaVariableTypeConstants.DIVIDE:
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

        /// <summary>
        /// Formül tanımına göre, formül değerini hesaplar.
        /// </summary>
        /// <param name="formulaDetails">Formül tanımının bulunduğu detaylar</param>
        /// <param name="width1">Formül hesaplamada kullanılacak En-1 bilgisi</param>
        /// <param name="width2">Formül hesaplamada kullanılacak En-2 bilgisi</param>
        /// <param name="width3">Formül hesaplamada kullanılacak En-3 bilgisi</param>
        /// <param name="height">Formül hesaplamada kullanılacak Yükseklik bilgisi</param>
        /// <returns></returns>
        public async Task<decimal> ExecuteFormula(List<FormulaDetail>? formulaDetails, decimal? width1, decimal? width2, decimal? width3, decimal? height)
        {
            string formulaText = string.Empty;

            if (formulaDetails == null)
            {
                throw new Exception($"Formül Hatası: Formül detaylarına ulaşılamadı.");
            }

            try
            {
                foreach (FormulaDetail formulaDetail in formulaDetails)
                {
                    switch (formulaDetail.VariableType)
                    {
                        case FormulaVariableTypeConstants.WIDTH1:
                            formulaText = formulaText + $" {width1}";
                            break;
                        case FormulaVariableTypeConstants.WIDTH2:
                            formulaText = formulaText + $" {width2}";
                            break;
                        case FormulaVariableTypeConstants.WIDTH3:
                            formulaText = formulaText + $" {width3}";
                            break;
                        case FormulaVariableTypeConstants.HEIGHT:
                            formulaText = formulaText + $" {height}";
                            break;
                        case FormulaVariableTypeConstants.RESULTVARIABLE:
                            var formula = await _repository.GetByIdAsync(formulaDetail.FormulId ?? "");
                            formulaText = formulaText + $" {ExecuteFormula(formula.FormulaDetails, width1, width2, width3, height)}";
                            break;
                        case FormulaVariableTypeConstants.CONSTANT:
                            formulaText = formulaText + $" {formulaDetail.VariableValue?.ToString() ?? ""}";
                            break;
                        case FormulaVariableTypeConstants.OPENPARENTHESIS:
                            formulaText = formulaText + " (";
                            break;
                        case FormulaVariableTypeConstants.CLOSEPARENTHESIS:
                            formulaText = formulaText + " )";
                            break;
                        case FormulaVariableTypeConstants.PLUS:
                            formulaText = formulaText + " +";
                            break;
                        case FormulaVariableTypeConstants.MINUS:
                            formulaText = formulaText + " -";
                            break;
                        case FormulaVariableTypeConstants.MULTIPLY:
                            formulaText = formulaText + " *";
                            break;
                        case FormulaVariableTypeConstants.DIVIDE:
                            formulaText = formulaText + " /";
                            break;
                    }
                }

                NCalc.Expression e = new NCalc.Expression(formulaText.Trim());

                var formulaValue = (decimal)e.Evaluate();

                return formulaValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Formül Hatası: {formulaText}");
            }
        }
    }
}
