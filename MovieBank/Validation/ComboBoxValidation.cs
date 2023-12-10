using System.Globalization;
using System.Windows.Controls;

namespace MovieBank.Validation
{
    public class ComboBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(value?.ToString()) && value.ToString() != "0")
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, "لطفا یک گزینه را انتخاب کنید!");
        }
    }
}