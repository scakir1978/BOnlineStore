﻿using BOnlineStore.Services.Production.Api.Dtos;

namespace BOnlineStore.Production.Api.Services
{
    public interface IDefinitionsService
    {
        Task<List<DefinitionsResponseDto>> GetByIdAsync(List<DefinitionsRequestDto> definitionsRequestList);
        Task<List<RawMaterialDto>> RawMaterialLoadFromList(List<string> rawMaterialIds);
    }
}
