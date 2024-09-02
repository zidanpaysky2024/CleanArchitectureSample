using Architecture.Application.Users.Commands.CreateUser;
using Architecture.Application.Users.Commands.Login;
using Architecture.WebAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebAPI.Controllers
{
    public class UserController : BaseApiController
    {
        #region Actions
        [HttpPost("user-login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginCommand loginCommand)
        {
            return Result(await Mediator.Send(loginCommand));
        }

        [HttpPost("Create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand loginCommand)
        {
            return Result(await Mediator.Send(loginCommand));
        }
        #endregion
    }
}
