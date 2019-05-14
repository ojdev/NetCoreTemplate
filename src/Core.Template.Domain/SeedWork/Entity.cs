using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Template.Domain.SeedWork
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Entity<TKey> : IEntity where TKey : struct
    {
        int? _requestedHashCode;
        TKey _Id;
        /// <summary>
        /// 
        /// </summary>
        public virtual TKey Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        /// <summary>
        /// 租户(CityId)
        /// </summary>
        public string TenantId { get; set; }
        private List<INotification> _domainEvents;
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return object.Equals(this.Id, default(TKey));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity<TKey> item = (Entity<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return object.Equals(item.Id, this.Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (IsTransient())
            {
                return base.GetHashCode();
            }
            else
            {

                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
