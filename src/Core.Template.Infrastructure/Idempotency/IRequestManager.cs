using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Template.Infrastructure.Idempotency
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="aggregateId"></param>
        /// <param name="event"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task CreateRequestForCommandAsync<T>(Guid id, Guid aggregateId, string @event, string user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        List<ClientRequest> GetByAggregateId(Guid aggregateId);
    }
}
