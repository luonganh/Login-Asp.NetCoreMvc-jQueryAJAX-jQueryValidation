using Microsoft.AspNetCore.Mvc.ModelBinding;
using Asp.NetCore.Shared.Helpers;

namespace Asp.NetCore.Services.Helpers
{
    public static class Utilities
    {
        public static Dictionary<string, string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, string>();
            for (int i = 0; i < modelState.Count; i++)
            {
                var stateObj = modelState.Values.ElementAtOrDefault(i);
                var error = stateObj.Errors.FirstOrDefault();
                if (error == null) continue;

                var errorKey = modelState.Keys.ElementAtOrDefault(i);
                var erroKeyStyle = errorKey.ToCamelCase();
                errors[erroKeyStyle] = error.ErrorMessage;
            }
            return errors;
        }
    }
}
