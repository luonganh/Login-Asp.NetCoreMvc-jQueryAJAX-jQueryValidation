using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AtLeastOneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                // must contains at least one number
                if (!str.Any(char.IsDigit))
                {
                    return new ValidationResult($"Must contains at least one number please.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
