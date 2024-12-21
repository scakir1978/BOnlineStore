using BOnlineStore.Localization;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Extensions;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Production.Api.Services.Definitions
{
    public class DefinitionsService : IDefinitionsService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public DefinitionsService(HttpClient client, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Language> stringLocalizer)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<List<DefinitionsResponseDto>> GetByIdAsync(List<DefinitionsRequestDto> definitionsRequestList)
        {
            var response = await _client.PostParameterizedAsync(DefinitionsApiControllerConstants.DefinitionsRequest, null,
                                                         definitionsRequestList, _httpContextAccessor, _stringLocalizer);

            //Geri dönen bilgi dto nesnesine dönüştürülür.
            var definitionResponseResult = await response.Content.ReadAsJsonAsync<List<DefinitionsResponseDto>>();

            return definitionResponseResult?.Result ?? new List<DefinitionsResponseDto>();
        }

        public async Task<List<RawMaterialDto>> RawMaterialLoadFromList(List<string> rawMaterialIds)
        {
            var response = await _client.PostParameterizedAsync(DefinitionsApiControllerConstants.RawMaterial, DefinitionsApiControllerFuctionsConstants.LoadFromList,
                                                         rawMaterialIds, _httpContextAccessor, _stringLocalizer);

            //Geri dönen bilgi dto nesnesine dönüştürülür.
            var definitionResponseResult = await response.Content.ReadAsJsonAsync<List<RawMaterialDto>>();

            return definitionResponseResult?.Result ?? new List<RawMaterialDto>();
        }
    }
}
