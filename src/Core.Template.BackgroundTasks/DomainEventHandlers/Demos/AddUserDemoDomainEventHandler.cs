using Core.Template.Domain.DomainEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Template.BackgroundTasks.DomainEventHandlers
{
    class AddUserDemoDomainEventHandler : INotificationHandler<AddUserDemoDomainEvent>
    {
        public Task Handle(AddUserDemoDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{notification.Name}已经{notification.Age}岁了");
            return Task.CompletedTask;
        }
    }
}
