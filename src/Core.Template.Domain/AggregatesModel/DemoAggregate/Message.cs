using Core.Template.Domain.DomainEvents;
using Core.Template.Domain.SeedWork;
using System;

namespace Core.Template.Domain.AggregatesModel
{
    /// <summary>
    /// 演示实体=>用户发送的消息
    /// </summary>
    public class Message : AuditEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual UserDemo User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public Message(string content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
        /// <summary>
        /// 
        /// </summary>
        public Message()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="user"></param>
        public Message(string content, UserDemo user) : this(content)
        {
            UserId = user.Id;
            AddDomainEvent(new UserSendMessageDomainEvent(user.Name, content));
        }
    }
}
