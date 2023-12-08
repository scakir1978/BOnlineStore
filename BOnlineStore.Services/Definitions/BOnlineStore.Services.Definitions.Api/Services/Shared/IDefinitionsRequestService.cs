using BOnlineStore.Services.Definitions.Api.Dtos;

namespace BOnlineStore.Services.Definitions.Api.Services.Shared
{
    public interface IDefinitionsRequestService
    {
        Task<List<DefinitionResponseDto>> GetByIdAsync(List<DefinitionsRequestDto> definitionsRequests);
    }
}
