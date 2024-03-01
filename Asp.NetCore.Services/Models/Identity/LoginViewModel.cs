using Asp.NetCore.Services.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore.Services.Models.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter the username")]
        [CustomRange(4, 50)]
        [NoWhiteSpacesAtBeginningAndEnd]
        [StartWithLetter]
        [EnglishCharsUnderscores]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter the password")]
        [CustomRange(6, 30)]
        [NoWhiteSpacesAtBeginningAndEnd]
        [StartWithLetter]
        [AtLeastOneNumber]
        [AtLeastOneSpecialCharacter]
        [AtLeastOneUppercaseLetterOneLowercaseLetter]
        public string? Password { get; set; }

        public bool RememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; }
    }
}
