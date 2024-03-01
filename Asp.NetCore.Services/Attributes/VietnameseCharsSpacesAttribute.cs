using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Asp.NetCore.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class VietnameseCharsSpacesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {                
                string pattern = @"^[0123456789qwertyuiopQWERTYUIOPèéẻẽẹÈÉẺẼẸềếểễệỀẾỂỄỆỳýỷỹỵỲÝỶỸỴùúủũụÙÚỦŨỤìíỉĩịÌÍỈĨỊòóỏõọÒÓỎÕỌồốổỗộỒỐỔỖỘaAàáảãạÀÁẢÃẠằắẳẵặẰẮẮẴẶầấẩẫậẦẤẨẪẬsSdDđĐfFgGhHjJkKlLzZxXcCvVbBnNmM\s]+$";
                Match startWithLetter = new Regex(pattern).Match(str);
                if (!startWithLetter.Success)
                {
                    return new ValidationResult($"Please enter only Vietnamese characters, numbers and spaces.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
