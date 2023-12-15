using BOnlineStore.Services.BFF.Api.Dtos;

namespace BOnlineStore.BFF.Api.Services.Definitions
{
    public interface IDefinitionsService
    {
        Task<List<DefinitionsResponseDto>> GetByIdAsync(List<DefinitionsRequestDto> definitionsRequestList);
        Task<List<RawMaterialDto>> RawMaterialLoadFromList(List<string> rawMaterialIds);
    }
}
