namespace ElectionCalc.ValidationRules
{
    using System.Globalization;
    using System.Linq;
    using System.Windows.Controls;

    class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var name = value as string;

            if(name.Length == 0) return new ValidationResult(false, "You have to fill in this field");
            else if(!name.All(char.IsLetter)) return new ValidationResult(false, "You can use letters only");

            return new ValidationResult(true, null);
        }
    }
}
