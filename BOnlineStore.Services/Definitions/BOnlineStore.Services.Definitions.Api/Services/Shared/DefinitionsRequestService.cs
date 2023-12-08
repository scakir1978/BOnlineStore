using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Helpers;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace BOnlineStore.Services.Definitions.Api.Services.Shared
{
    public class DefinitionsRequestService : IDefinitionsRequestService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStringLocalizer<Language> _stringLocalizer;

        public DefinitionsRequestService(IServiceProvider serviceProvider, IStringLocalizer<Language> stringLocalizer)
        {
            _serviceProvider = serviceProvider;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<List<DefinitionResponseDto>> GetByIdAsync(List<DefinitionsRequestDto> definitionsRequests)
        {
            if (definitionsRequests == null) throw new ArgumentNullException(nameof(definitionsRequests), _stringLocalizer[SharedKeys.DefinitionsRequestDtoCannotBeNull]);

            var definitionResponseDtoList = new List<DefinitionResponseDto>();

            foreach (DefinitionsRequestDto definition in definitionsRequests)
            {
                if (string.IsNullOrWhiteSpace(definition.EntityName))
                    throw new ArgumentNullException(nameof(definition.EntityName), _stringLocalizer[SharedKeys.DefinitionsEntityNameCannotBeNull]);

                if (string.IsNullOrWhiteSpace(definition.EntityId))
                    throw new ArgumentNullException(nameof(definition.EntityId), _stringLocalizer[SharedKeys.DefinitionsEntityIdCannotBeNull]);

                var entity = await RunDIServiceFunction.GetByIdAsync(_serviceProvider, definition.EntityName, definition.EntityId);

                var definitionResponseDto = new DefinitionResponseDto { Entity = entity, EntityName = definition.EntityName };

                definitionResponseDtoList.Add(definitionResponseDto);

            }

            return definitionResponseDtoList;
        }
    }

}
