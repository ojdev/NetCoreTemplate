using Core.Template.Application.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Template
{
    /// <summary>
    /// 
    /// </summary>
    public static class IMediatorExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mediator"></param>
        /// <param name="request"></param>
        /// <param name="requestId"></param>
        /// <param name="aggregateId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TResult> IdempotencySendAsync<TResult>(this IMediator mediator, IRequest<TResult> request, string requestId, Guid aggregateId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var identified = new IdentifiedCommand<IRequest<TResult>, TResult>(request, guid, aggregateId);
                return await mediator.Send(identified, cancellationToken);
            }
            return default;
        }
    }
}
