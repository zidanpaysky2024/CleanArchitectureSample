using MediatR;

namespace Architecture.Application.Common.Messaging
{
    public interface IBaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
         where TRequest : IBaseRequest<TResponse>
    {
    }

}
