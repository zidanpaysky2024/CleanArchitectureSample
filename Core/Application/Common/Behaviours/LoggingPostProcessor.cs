﻿using Architecture.Application.Common.Messaging;
using System.Diagnostics;

namespace Architecture.Application.Common.Behaviours
{
    public class LoggingPostProcessor<TRequest> : IRequestPostProcessor<TRequest>
        where TRequest : notnull, IBaseCommand
    {
        public Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Post processor");

            return Task.CompletedTask;
        }
    }
}
