using MediatR;
using System;

namespace Core.Template.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="RCommand"></typeparam>
    public class IdentifiedCommand<TCommand, RCommand> : IRequest<RCommand> where TCommand : IRequest<RCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        public TCommand Command { get; }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// 聚合根Id
        /// </summary>
        public Guid AggregateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <param name="aggregateId"></param>
        public IdentifiedCommand(TCommand command, Guid id, Guid aggregateId)
        {
            Command = command;
            Id = id;
            AggregateId = aggregateId;
        }
    }
}
