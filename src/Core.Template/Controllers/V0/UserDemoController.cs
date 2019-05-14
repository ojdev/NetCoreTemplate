using Core.Template.Application.Commands;
using Core.Template.Controllers.Abstractions;
using Core.Template.Domain.DomainEvents;
using Core.Template.Models;
using GenFu;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Core.Template.Controllers.V0
{
    /// <summary>
    /// 测试
    /// </summary>
    [ApiVersion("0")]
    public class UserDemoController : UserDemoControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public UserDemoController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public override async Task<IActionResult> Add([FromBody] AddUserDemoCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            return Ok(await Task.FromResult(true));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public override Task<IActionResult> EditName(Guid id, [FromBody] SendMessageCommand command)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<IActionResult> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IActionResult> GetPersons()
        {
            return Ok(await Task.FromResult(A.ListOf<DemoPersonOutput>(10000)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override async Task<IActionResult> Send([FromBody] SendMessageCommand command)
        {
            await _mediator.Publish(new UserSendMessageDomainEvent("demouser", "测试消息"));
            return Ok(true);
        }
    }
}
