using AutoMapper;
using BOnlineStore.Generic.Service;
using BOnlineStore.Localization;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using BOnlineStore.Services.Definitions.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public class DeliveryAdressService : Service<DeliveryAdress, DeliveryAdressDto, DeliveryAdressCreateDto, DeliveryAdressUpdateDto>, IDeliveryAdressService
    {
        public DeliveryAdressService(
            IDeliveryAdressRepository repository,
            IMapper mapper,
            IStringLocalizer<Language> stringLocalizer,
            IValidator<DeliveryAdress> validator) : base(repository, mapper, stringLocalizer, validator)
        { }

    }
}
