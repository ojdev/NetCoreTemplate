using Core.Template.Domain.DomainEvents;
using Core.Template.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Template.Domain.AggregatesModel
{
    /// <summary>
    /// 演示实体=>用户
    /// </summary>
    public class UserDemo : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Age { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public UserDemo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        public UserDemo(string name, int? age)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age ?? throw new ArgumentNullException(nameof(age));
            AddDomainEvent(new AddUserDemoDomainEvent(name, age));
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public void Send(string content)
        {
            Messages.Add(new Message(content));
            AddDomainEvent(new UserSendMessageDomainEvent(Name, content));
        }
    }
}
