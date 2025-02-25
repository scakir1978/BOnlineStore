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
using System.Globalization;
using System.Text;

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

        public async Task<bool> CopyFormula(string formulaId, string formulaCode, string modelId, string panelId)
        {
            ValidateInput(formulaId, formulaCode, modelId, panelId);

            var formula = await _repository.GetByIdAsync(formulaId);

            if (formula == null)
            {
                throw new InvalidOperationException(_stringLocalizer[ProductionApiKeys.FormulaIdNotFound]);
            }

            var formulaCreateDto = _mapper.Map<FormulaCreateDto>(formula);
            formulaCreateDto.Id = formula.GetNewId();
            formulaCreateDto.Code = formulaCode;
            formulaCreateDto.ModelId = string.IsNullOrWhiteSpace(modelId) ? null : modelId;
            formulaCreateDto.PanelId = string.IsNullOrWhiteSpace(panelId) ? null: panelId;

            await AddAsync(formulaCreateDto);

            return true;
        }

        public async Task<bool> ExecuteFormulaTest(List<FormulaDetail> formulaDetails)
        {
            try
            {
                var formulaText = BuildFormulaText(formulaDetails);
                var expression = new NCalc.Expression(formulaText.Trim());
                expression.Evaluate();
                return await Task.FromResult(true);
            }
            catch (NCalc.Exceptions.NCalcEvaluationException)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<decimal> ExecuteFormula(List<FormulaDetail>? formulaDetails, decimal? width1, decimal? width2, decimal? width3, decimal? height)
        {
            if (formulaDetails == null)
            {
                throw new ArgumentException("Formula details cannot be null.");
            }

            string formulaText = string.Empty;

            try
            {
                formulaText = await BuildFormulaTextAsync(formulaDetails, width1, width2, width3, height);
                var expression = new NCalc.Expression(formulaText.Trim());
                var formulaValue = Convert.ToDecimal(expression.Evaluate());
                return formulaValue;
            }
            catch (NCalc.Exceptions.NCalcEvaluationException ex)
            {
                throw new InvalidOperationException($"Formula Error: ({formulaText}) Formula ID: {formulaDetails.FirstOrDefault()?.FormulId ?? ""}", ex);
            }
        }

        private void ValidateInput(string formulaId, string formulaCode, string modelId, string panelId)
        {
            if (string.IsNullOrWhiteSpace(formulaId))
                throw new ArgumentException(_stringLocalizer[ProductionApiKeys.FormulaIdNotEmpty]);

            if (string.IsNullOrWhiteSpace(formulaCode))
                throw new ArgumentException(_stringLocalizer[ProductionApiKeys.FormulaCodeNotEmpty]);

            if (!string.IsNullOrWhiteSpace(modelId) && !string.IsNullOrWhiteSpace(panelId))
                throw new ArgumentException(_stringLocalizer[ProductionApiKeys.ModelIdAndPanelIdCannotBeBothSet]);
        }

        private string BuildFormulaText(List<FormulaDetail> formulaDetails)
        {
            var formulaText = new StringBuilder();

            foreach (var formulaDetail in formulaDetails)
            {
                switch (formulaDetail.VariableType)
                {
                    case FormulaVariableTypeConstants.WIDTH1:
                    case FormulaVariableTypeConstants.WIDTH2:
                    case FormulaVariableTypeConstants.WIDTH3:
                    case FormulaVariableTypeConstants.HEIGHT:
                    case FormulaVariableTypeConstants.RESULTVARIABLE:
                        formulaText.Append(" 100");
                        break;
                    case FormulaVariableTypeConstants.CONSTANT:
                        formulaText.Append($" {formulaDetail.VariableValue?.ToString().Replace(",", ".") ?? ""}");
                        break;
                    case FormulaVariableTypeConstants.OPENPARENTHESIS:
                        formulaText.Append(" (");
                        break;
                    case FormulaVariableTypeConstants.CLOSEPARENTHESIS:
                        formulaText.Append(" )");
                        break;
                    case FormulaVariableTypeConstants.PLUS:
                        formulaText.Append(" +");
                        break;
                    case FormulaVariableTypeConstants.MINUS:
                        formulaText.Append(" -");
                        break;
                    case FormulaVariableTypeConstants.MULTIPLY:
                        formulaText.Append(" *");
                        break;
                    case FormulaVariableTypeConstants.DIVIDE:
                        formulaText.Append(" /");
                        break;
                }
            }

            return formulaText.ToString();
        }

        private async Task<string> BuildFormulaTextAsync(List<FormulaDetail> formulaDetails, decimal? width1, decimal? width2, decimal? width3, decimal? height)
        {
            var formulaText = new StringBuilder();

            foreach (var formulaDetail in formulaDetails)
            {
                switch (formulaDetail.VariableType)
                {
                    case FormulaVariableTypeConstants.WIDTH1:
                        formulaText.Append($" {width1}");
                        break;
                    case FormulaVariableTypeConstants.WIDTH2:
                        formulaText.Append($" {width2}");
                        break;
                    case FormulaVariableTypeConstants.WIDTH3:
                        formulaText.Append($" {width3}");
                        break;
                    case FormulaVariableTypeConstants.HEIGHT:
                        formulaText.Append($" {height}");
                        break;
                    case FormulaVariableTypeConstants.RESULTVARIABLE:
                        var formula = await _repository.GetByIdAsync(formulaDetail.FormulId ?? "");
                        var result = await ExecuteFormula(formula.FormulaDetails, width1, width2, width3, height);
                        formulaText.Append($" {result.ToString().Replace(",", ".")}");
                        break;
                    case FormulaVariableTypeConstants.CONSTANT:
                        formulaText.Append($" {formulaDetail.VariableValue?.ToString().Replace(",", ".") ?? ""}");
                        break;
                    case FormulaVariableTypeConstants.OPENPARENTHESIS:
                        formulaText.Append(" (");
                        break;
                    case FormulaVariableTypeConstants.CLOSEPARENTHESIS:
                        formulaText.Append(" )");
                        break;
                    case FormulaVariableTypeConstants.PLUS:
                        formulaText.Append(" +");
                        break;
                    case FormulaVariableTypeConstants.MINUS:
                        formulaText.Append(" -");
                        break;
                    case FormulaVariableTypeConstants.MULTIPLY:
                        formulaText.Append(" *");
                        break;
                    case FormulaVariableTypeConstants.DIVIDE:
                        formulaText.Append(" /");
                        break;
                }
            }

            return formulaText.ToString();
        }
    }
}
