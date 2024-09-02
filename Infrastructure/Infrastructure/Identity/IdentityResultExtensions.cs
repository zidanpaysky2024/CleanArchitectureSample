using Architecture.Application.Common.Errors;
using Architecture.Application.Common.Messaging;
using Microsoft.AspNetCore.Identity;

namespace Architecture.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Response<bool> ToApplicationResult(this IdentityResult result)
        {

            var errors = result.Errors
                 .Select(e => new { e.Code, e.Description })
                 .ToDictionary(e => e.Code, e => e.Description);

            return result.Succeeded
                ? Response.Success(true)
                : Response.Failure(SecurityAccessErrors.CreationUserFailed(errors));
        }
    }
}