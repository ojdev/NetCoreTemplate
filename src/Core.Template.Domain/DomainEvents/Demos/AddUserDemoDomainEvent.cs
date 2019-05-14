using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Template.Domain.DomainEvents
{
    /// <summary>
    /// 添加用户时产生的事件
    /// </summary>
    public class AddUserDemoDomainEvent : INotification
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        public AddUserDemoDomainEvent(string name, int? age)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age ?? throw new ArgumentNullException(nameof(age));
        }
    }
}
