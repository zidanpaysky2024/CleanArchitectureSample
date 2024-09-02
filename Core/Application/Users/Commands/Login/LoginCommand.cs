using Architecture.Application.Common.Abstracts.Account;
using Architecture.Application.Common.Abstracts.Persistence;
using Architecture.Application.Common.Messaging;
using Architecture.Application.Users.Commands.Dtos;

namespace Architecture.Application.Users.Commands.Login
{
    #region Request
    public record LoginCommand(string Password) : BaseCommand<TokenResponse>
    {
        public new required string UserName { get; init; }
    }

    #endregion

    #region Request Handler
    public class LoginCommandHandler : BaseCommandHandler<LoginCommand, TokenResponse>
    {
        #region Dependencies
        private IIdentityService IdentityService { get; }

        #endregion

        #region Constructor
        public LoginCommandHandler(IServiceProvider serviceProvider,
                                   IApplicationDbContext dbContext,
                                   IIdentityService identityService)
           : base(serviceProvider, dbContext)
        {
            IdentityService = identityService;
        }
        #endregion

        #region Request Handle
        public override async Task<Response<TokenResponse>> HandleRequest(LoginCommand request,
                                                                           CancellationToken cancellationToken)
        {
            return await IdentityService.GetTokenAsync(request.UserName!);
        }


        #endregion
    }
    #endregion
}
