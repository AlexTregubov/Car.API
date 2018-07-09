using Car.Api.Model;
using FluentValidation;

namespace Car.Api.Validation
{
    public class CreateCarModelValidator : BaseModelValidator<CreateCarModel>
    {
        public CreateCarModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleFor(x => x.RegistrationNumber)
                .NotNull().NotEmpty()
                .SetValidator(x => new RegistrationNumberValidator());
        }
    }
}
