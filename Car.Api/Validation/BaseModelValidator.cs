using System;
using FluentValidation;

namespace Car.Api.Validation
{

    public abstract class BaseModelValidator<T> : AbstractValidator<T>
    {
        protected BaseModelValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }

        protected string InvalidEnumValue<TEnum>()
        {
            var values = string.Join("', '", Enum.GetNames(typeof(TEnum)));
            return $"Invalid '{{PropertyName}}' value. Possible values: '{values}'";
        }

        protected string InvalidValue()
        {
            return "Invalid '{PropertyName}' value.";
        }

        protected string PropertyShouldNotEmpty()
        {
            return "'{PropertyName}' should not be empty";
        }
    }
}
