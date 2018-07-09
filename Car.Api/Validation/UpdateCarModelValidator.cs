using Car.Api.Model;
using FluentValidation;

namespace Car.Api.Validation
{
    public class UpdateCarModelValidator : BaseModelValidator<UpdateCarModel>
    {
        public UpdateCarModelValidator()
        {
            RuleFor(x => x.RegistrationNumber)
                .Must(x => ValidateNumber(x));
        }

        private bool ValidateNumber(string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                return true;
            }

            return new RegistrationNumberValidator().Validate(x).IsValid;
        }
    }
}
