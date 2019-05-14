using Core.Template.BackgroundTasks.Subscribes;
using Core.Template.Domain.DomainEvents;
using MediatR;
using RabbitMQ.EventBus.AspNetCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Template.BackgroundTasks.DomainEventHandlers
{
    class UserSendMessageDomainEventHandler : INotificationHandler<UserSendMessageDomainEvent>
    {
        private readonly IRabbitMQEventBus _rabbitMQ;

        public UserSendMessageDomainEventHandler(IRabbitMQEventBus rabbitMQ)
        {
            _rabbitMQ = rabbitMQ ?? throw new ArgumentNullException(nameof(rabbitMQ));
        }

        public Task Handle(UserSendMessageDomainEvent notification, CancellationToken cancellationToken)
        {
            _rabbitMQ.Publish(new DemoSubscribe { Name = notification.UserName }, "event.exchange", "demo.key");
            Console.WriteLine($"{notification.UserName}广播了消息【{notification.Content}】");
            return Task.CompletedTask;
        }
    }
}
