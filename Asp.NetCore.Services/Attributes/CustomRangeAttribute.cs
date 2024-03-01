using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CustomRangeAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public CustomRangeAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str) && str.Length < _minLength)
            {
                return new ValidationResult($"Requires a minimum length of {_minLength} characters.");
            }
            if (!string.IsNullOrEmpty(str) && str.Length > _maxLength)
            {
                return new ValidationResult($"Requires a maximum length of {_maxLength} characters.");
            }
            return ValidationResult.Success;
        }
    }
}
