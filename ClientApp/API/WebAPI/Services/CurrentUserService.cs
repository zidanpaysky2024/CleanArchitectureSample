using CleanArchitecture.Application.Common.Abstracts.Account;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using static Azure.Core.HttpHeader;

namespace CleanArchitecture.WebAPI.Services
{
    public class CurrentUser : ICurrentUser
    {
        #region Dependencies
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Properties
        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid") ?? "Anonymous";

        public string Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Name) ?? "Anonymous";
        #endregion

    }
}
