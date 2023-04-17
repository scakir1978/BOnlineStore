using AutoMapper;
using BOnlineStore.Services.Definitions.Api.Dtos;
using BOnlineStore.Services.Definitions.Api.Entities;

namespace BOnlineStore.Services.Definitions.Api.Mappings
{
    public class ExpenseMappings : Profile
    {
        public ExpenseMappings()
        {
            CreateMap<Expense, ExpenseDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Expense, ExpenseCreateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation();
            CreateMap<Expense, ExpenseUpdateDto>().DisableCtorValidation().ReverseMap().DisableCtorValidation().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }

    }
}
