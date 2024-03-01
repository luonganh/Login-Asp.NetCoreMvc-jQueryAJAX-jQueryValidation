using Asp.NetCore.Infrastructure.Identity.Enums;
using Asp.NetCore.Services.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Asp.NetCore.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IdentityContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IdentityContext context, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(ModelStateDictionary modelStates, LoginViewModel model)
        {
            bool isValid = false;
            var user = await _userManager.FindByNameAsync(model?.Username?.Trim() ?? string.Empty);
            if (user == null)
            {
                modelStates.AddModelError(nameof(LoginViewModel.Username), "The account does not exist.");
                return await Task.FromResult(isValid);
            }

            if (!Enum.IsDefined(typeof(AppUserStatus), user.Status))
            {
                modelStates.AddModelError(nameof(LoginViewModel.Username), "Account status is invalid. Valid statuses: unactived, actived, disabled.");
                return await Task.FromResult(isValid);
            }

            switch (user.Status)
            {
                case AppUserStatus.Unactived:
                    modelStates.AddModelError(nameof(LoginViewModel.Username), "Account status is invalid: Unactived.");
                    return await Task.FromResult(isValid);
                case AppUserStatus.Disabled:
                    modelStates.AddModelError(nameof(LoginViewModel.Username), "Account status is invalid: Disabled.");
                    return await Task.FromResult(isValid);
            }

            var loginRes = await _signInManager.PasswordSignInAsync(model?.Username?.Trim() ?? string.Empty, model?.Password?.Trim() ?? string.Empty, true, lockoutOnFailure: true);
            if (!loginRes.Succeeded)
            {
                modelStates.AddModelError(nameof(LoginViewModel.Password), "Password is wrong.");
            }

            if (modelStates.ErrorCount == 0)
            {
                isValid = true;
            }
            return await Task.FromResult(isValid);
        }
    }
}