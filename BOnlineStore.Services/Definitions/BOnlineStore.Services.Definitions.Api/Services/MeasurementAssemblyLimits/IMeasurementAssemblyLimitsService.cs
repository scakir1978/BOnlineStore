﻿using BOnlineStore.Generic.Service;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using System.Linq.Expressions;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public interface IMeasurementAssemblyLimitsService : IService<MeasurementAssemblyLimits, MeasurementAssemblyLimitsDto, MeasurementAssemblyLimitsCreateDto, MeasurementAssemblyLimitsUpdateDto>
    {
    }
}
