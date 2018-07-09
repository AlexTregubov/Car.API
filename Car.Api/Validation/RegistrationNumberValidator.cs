using System.Linq;
using FluentValidation;

namespace Car.Api.Validation
{
    public class RegistrationNumberValidator : BaseModelValidator<string>
    {
        public RegistrationNumberValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty()
                .Must(IsRegistrationNumberValid);
        }

        private static bool IsRegistrationNumberValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            if (number.Length < 8 || number.Length > 9)
            {
                return false;
            }

            if (!char.IsLetter(number[0]))
            {
                return false;
            }

            if (!number.Substring(1,3).All(x => char.IsDigit(x)))
            {
                return false;
            }

            if (!number.Substring(4, 2).All(x => char.IsLetter(x)))
            {
                return false;
            }

            if (!number.Substring(6).All(x => char.IsDigit(x)))
            {
                return false;
            }

            return true;
        }
    }
}
