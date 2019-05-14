using MediatR;
using System;

namespace Core.Template.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class SendMessageCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

    }
}
