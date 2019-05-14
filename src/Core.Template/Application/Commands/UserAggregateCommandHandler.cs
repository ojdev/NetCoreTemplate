using Core.Template.Domain.AggregatesModel;
using Core.Template.Infrastructure;
using Core.Template.Infrastructure.Idempotency;
using Core.Template.Infrastructure.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Template.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAggregateCommandHandler : IRequestHandler<SendMessageCommand, bool>, IRequestHandler<AddUserDemoCommand, bool>
    {
        private readonly TemplateContext _context;
        private readonly IIdentityService _identity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identity"></param>
        public UserAggregateCommandHandler(TemplateContext context, IIdentityService identity)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.UserDemos.FindAsync(request.Id);
            if (user != null)
            {
                user.Send(request.Content);
                return await _context.SaveEntitiesAsync();
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AddUserDemoCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(_identity.CurrentUser.City);
            await _context.UserDemos.AddAsync(new UserDemo(request.Name, request.Age));
            return await _context.SaveEntitiesAsync();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AddUserDemoCommandIdentifiedCommandHandler : IdentifiedCommandHandler<AddUserDemoCommand, bool>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="requestManager"></param>
        /// <param name="identityService"></param>
        public AddUserDemoCommandIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager, IIdentityService identityService) : base(mediator, requestManager, identityService)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}
