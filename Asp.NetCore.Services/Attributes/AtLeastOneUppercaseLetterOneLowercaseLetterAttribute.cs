using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AtLeastOneUppercaseLetterOneLowercaseLetterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                // must contains at least one uppercase letter and one lowercase letter
                Match atLeastOneUppercaseLetterOneLowercaseLetter = new Regex(@"^(?=.*[a-z])(?=.*[A-Z]).+$").Match(str);
                if (!atLeastOneUppercaseLetterOneLowercaseLetter.Success)
                {
                    return new ValidationResult($"Must contains at least one uppercase letter and at least one lowercase letter please.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
