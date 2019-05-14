using RabbitMQ.EventBus.AspNetCore.Attributes;
using RabbitMQ.EventBus.AspNetCore.Events;
using System;
using System.Threading.Tasks;

namespace Core.Template.BackgroundTasks.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [EventBus(Exchange = "event.exchange", RoutingKey = "demo.key")]
    public class DemoSubscribe : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class DemoSubscribeHandle : IEventHandler<DemoSubscribe>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task Handle(EventHandlerArgs<DemoSubscribe> args)
        {
            Console.WriteLine(args.Event.Name);
            Console.WriteLine(args.Exchange);
            Console.WriteLine(args.RoutingKey);
            Console.WriteLine(args.Redelivered);
            Console.WriteLine(args.Original);
            return Task.CompletedTask;
        }
    }
}
