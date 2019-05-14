using Core.Template.Application.Commands;
using Core.Template.Controllers.Abstractions;
using Core.Template.Infrastructure.Idempotency;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.Template.Controllers.V1
{
    /// <summary>
    /// 正式
    /// </summary>
    [ApiVersion("1")]
    public class DemoController : UserDemoControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public DemoController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// 新用户
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public override async Task<IActionResult> Add([FromBody] AddUserDemoCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = await _mediator.IdempotencySendAsync(command, requestId, Guid.Empty);
            if (!commandResult)
            {
                return BadRequest();
            }
            return Ok(commandResult);
        }

        public override Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> EditName(Guid id, [FromBody] SendMessageCommand command)
        {
            throw new NotImplementedException();
        }

        public override Task<IActionResult> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public override Task<IActionResult> GetPersons()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override async Task<IActionResult> Send([FromBody] SendMessageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
