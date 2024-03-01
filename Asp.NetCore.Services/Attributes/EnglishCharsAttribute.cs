using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnglishCharsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                // must start with letter
                Match startWithLetter = new Regex(@"^[a-zA-Z0-9]+$").Match(str);
                if (!startWithLetter.Success)
                {
                    return new ValidationResult($"Only accept English characters, numbers.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
