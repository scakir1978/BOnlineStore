using BOnlineStore.Generic.Service;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;
using System.Linq.Expressions;

namespace BOnlineStore.Services.Definitions.Api.Services
{
    public interface IBankService : IService<Bank, BankDto, BankCreateDto, BankUpdateDto>
    {
    }
}
