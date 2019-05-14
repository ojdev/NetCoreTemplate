using MediatR;
using System;

namespace Core.Template.Domain.DomainEvents
{
    /// <summary>
    /// 用户发送消息时产生的事件
    /// </summary>
    public class UserSendMessageDomainEvent : INotification
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="content"></param>
        public UserSendMessageDomainEvent(string userName, string content)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
