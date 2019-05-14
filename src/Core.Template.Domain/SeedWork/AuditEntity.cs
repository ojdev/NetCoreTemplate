using System;

namespace Core.Template.Domain.SeedWork
{
    /// <summary>
    /// 带审计的
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AuditEntity<TKey> : Entity<TKey> where TKey : struct
    {
        private DateTimeOffset _creationTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset CreationTime => _creationTime;
        private DateTimeOffset? _lastUpdateTime;
    }
}
