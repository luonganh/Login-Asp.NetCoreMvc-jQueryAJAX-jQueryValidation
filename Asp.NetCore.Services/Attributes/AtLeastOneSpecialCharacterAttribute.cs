using Asp.NetCore.Shared;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AtLeastOneSpecialCharacterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                // must contains at least one special character                        
                if (str.Any(x => Utilities.Characters.SpecialCharacter.Contains(x.ToString())) == false)
                {
                    return new ValidationResult($"Must contains at least one special character i.e: ~`!@#$%^&*()_-+={{[}}]|\\:;\"'<,>?/. please.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
