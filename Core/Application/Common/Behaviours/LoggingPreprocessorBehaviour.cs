using Architecture.Application.Common.Messaging;
using System.Diagnostics;

namespace Architecture.Application.Common.Behaviours
{
    public class LoggingPreprocessorBehaviour<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : notnull, IBaseQuery
    {
        public Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Preprocessor");
            return Task.CompletedTask;
        }
    }
}
