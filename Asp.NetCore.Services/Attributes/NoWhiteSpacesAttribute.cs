using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoWhiteSpacesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str) && str.Contains(" "))
            {
                return new ValidationResult($"Whitespace characters are not allowed at this field.");
            }
            return ValidationResult.Success;
        }
    }
}
