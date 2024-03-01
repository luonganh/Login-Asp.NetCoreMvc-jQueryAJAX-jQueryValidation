using Asp.NetCore.Services.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Asp.NetCore.Services.Identity
{
    public interface IIdentityService
    {
        Task<bool> LoginAsync(ModelStateDictionary modelStates, LoginViewModel model);
    }
}
