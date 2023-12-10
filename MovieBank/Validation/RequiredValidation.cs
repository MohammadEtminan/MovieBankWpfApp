using System.Globalization;
using System.Windows.Controls;

namespace MovieBank.Validation
{
    public class RequiredValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace(value?.ToString()))
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, "فیلد اجباری!");
        }
    }
}