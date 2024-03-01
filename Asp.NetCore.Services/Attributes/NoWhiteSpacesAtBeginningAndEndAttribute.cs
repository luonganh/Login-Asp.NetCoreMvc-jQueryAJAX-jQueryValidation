using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoWhiteSpacesAtBeginningAndEndAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str) && (str.StartsWith(" ") || str.EndsWith(" ")))
            {
                return new ValidationResult($"Whitespace characters are not allowed at begin and end of this field.");
            }
            return ValidationResult.Success;
        }
    }
}
