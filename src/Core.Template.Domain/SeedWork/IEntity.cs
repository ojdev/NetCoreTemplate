using MediatR;
using System.Collections.Generic;

namespace Core.Template.Domain.SeedWork
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyCollection<INotification> DomainEvents { get; }
        /// <summary>
        /// 
        /// </summary>
        void ClearDomainEvents();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        void RemoveDomainEvent(INotification eventItem);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEvent(INotification eventItem);
    }
}
