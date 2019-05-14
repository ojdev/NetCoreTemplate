using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Template.Infrastructure.Idempotency
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset DateTime { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid AggregateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Event { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string User { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected ClientRequest() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">TypeName</param>
        /// <param name="aggregateId">聚合根Id</param>
        /// <param name="event">事件内容(json)</param>
        /// <param name="user"></param>
        public ClientRequest(Guid id, string name, Guid aggregateId, string @event, string user)
        {
            Id = id;
            DateTime = DateTimeOffset.Now;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AggregateId = aggregateId;
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
            User = user;
        }
    }
}
