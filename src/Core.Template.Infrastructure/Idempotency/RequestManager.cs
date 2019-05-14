using Core.Template.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Template.Infrastructure.Idempotency
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestManager : IRequestManager
    {
        private readonly EventSourceContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RequestManager(EventSourceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="aggregateId"></param>
        /// <param name="event"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateRequestForCommandAsync<T>(Guid id, Guid aggregateId, string @event, string user)
        {
            var exists = await ExistAsync(id);
            var request = exists ? throw new DomainException($"Request with {id} already exists") : new ClientRequest(id, typeof(T).Name, aggregateId, @event, user);
            _context.Add(request);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(Guid id)
        {
            var request = await _context.FindAsync<ClientRequest>(id);
            return request != null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        public List<ClientRequest> GetByAggregateId(Guid aggregateId)
        {
            var request = _context.ClientRequests.AsNoTracking().Where(x => x.AggregateId == aggregateId).OrderByDescending(t => t.DateTime).ToList();
            return request;
        }
    }
}
