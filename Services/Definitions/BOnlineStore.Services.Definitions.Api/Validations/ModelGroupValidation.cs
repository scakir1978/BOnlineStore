using BOnlineStore.Services.Definitions.Api.Dtos;
using FluentValidation;

namespace BOnlineStore.Services.Definitions.Api.Validations
{
    public class ModelGroupCreateValidation : AbstractValidator<ModelGroupCreateDto>
    {
        public ModelGroupCreateValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
        
    }
    public class ModelGroupUpdateValidation : AbstractValidator<ModelGroupUpdateDto>
    {
        public ModelGroupUpdateValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }        
    }
}
