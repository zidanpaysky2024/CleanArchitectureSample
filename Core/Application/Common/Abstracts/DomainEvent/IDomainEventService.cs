using Architecture.Domain.Common;

namespace Architecture.Application.Common.Abstracts.DomainEvent
{
    public interface IDomainEventService
    {
        Task Publish(BaseDomainEvent domainEvent);
    }
}
