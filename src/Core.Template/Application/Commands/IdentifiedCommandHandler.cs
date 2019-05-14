using Core.Template.Infrastructure.Idempotency;
using Core.Template.Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Template.Application.Commands
{
    /// <summary>
    /// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
    /// a requestid sent by client is used to detect duplicate requests.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command handler that performs the operation if request is not duplicated</typeparam>
    /// <typeparam name="RCommand">Return value of the inner command handler</typeparam>
    public class IdentifiedCommandHandler<TCommand, RCommand> : IRequestHandler<IdentifiedCommand<TCommand, RCommand>, RCommand> where TCommand : IRequest<RCommand>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="requestManager"></param>
        /// <param name="identityService"></param>
        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, IIdentityService identityService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _requestManager = requestManager ?? throw new ArgumentNullException(nameof(requestManager));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }
        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual RCommand CreateResultForDuplicateRequest()
        {
            return default(RCommand);
        }

        /// <summary>
        /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
        /// just enqueues the original inner command.
        /// </summary>
        /// <param name="request">IdentifiedCommand which contains both original command and request ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Return value of inner command or default value if request same ID was found</returns>
        public async Task<RCommand> Handle(IdentifiedCommand<TCommand, RCommand> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(request.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await _requestManager.CreateRequestForCommandAsync<TCommand>(request.Id, request.AggregateId, JsonConvert.SerializeObject(request.Command), _identityService.CurrentUser.Serialize());
                try
                {
                    // Send the embeded business command to mediator so it runs its related CommandHandler 
                    var result = await _mediator.Send(request.Command);
                    return result;
                }
                catch
                {
                    return default(RCommand);
                }
            }
        }
    }
}
