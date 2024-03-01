namespace Asp.NetCore.Services.Models.Identity
{
    public class LoginViewModel
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public bool RememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; }
    }
}
