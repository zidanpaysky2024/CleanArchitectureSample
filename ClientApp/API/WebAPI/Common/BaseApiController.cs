﻿using CleanArchitecture.Application.Common.Messaging;
using Common.DependencyInjection.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Common
{
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        #region Dependencies

        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetInstance<IMediator>();
        #endregion

        #region Constructors
        public BaseApiController()
        {

        }
        #endregion

        #region Properties

        #endregion

        #region Method
        [NonAction]
        public ObjectResult Result<T>(IResponse<T> response)
        {
            return response.IsSuccess
                ? Ok(response)
                : Problem(response);
        }

        public ObjectResult Problem<T>(IResponse<T> response)
        {
            var customProblemDetails = new CustomProblemDetails
            {
                Title = response.Error!.Message,
                Status = response.StatusCode,
                Detail = response.Error?.Message,
                Instance = $"{response.Source} "
                           + $"Path:{HttpContext.Request.Path}/{HttpContext.Request.Method} "
                           + $"Host:{HttpContext.Request.Host} "
                           + $"Protocol:{HttpContext.Request.Protocol}",
                SubErrors = response.Error?.SubErrors ?? []
            };

            return new ObjectResult(customProblemDetails)
            {
                StatusCode = response.StatusCode
            };          
        }

        public class CustomProblemDetails : ProblemDetails
        {
            public Dictionary<string, string> SubErrors { get; set; } = [];
        }

        #endregion
    }
}
