namespace ElectionCalc.ValidationRules
{
    using System.Globalization;
    using System.Linq;
    using System.Windows.Controls;

    class PeselValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var number = value as string;

            if (number.Length != 11) return new ValidationResult(false, "PESEL number has to be correct length");
            else if (!number.All(char.IsDigit)) return new ValidationResult(false, "You can use digits only");
            else if (!PeselValidationTools.ValidPESEL(number)) return new ValidationResult(false, "You have to provide valid PESEL number");

            return new ValidationResult(true, null);
        }
    }
}
