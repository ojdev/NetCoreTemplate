using Core.Template.Application.Commands;
using Core.Template.Infrastructure.Filters;
using Core.Template.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Template.Controllers.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/users")]
    public abstract class UserDemoControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        protected UserDemoControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract Task<IActionResult> Add([FromBody] AddUserDemoCommand command, [FromHeader(Name = "x-requestid")] string requestId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("actions/send")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract Task<IActionResult> Send([FromBody] SendMessageCommand command);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<DemoPersonOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract Task<IActionResult> GetPersons();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<DemoPersonOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract Task<IActionResult> Get(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public abstract Task<IActionResult> EditName(Guid id, [FromBody] SendMessageCommand command);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public abstract Task<IActionResult> Delete(Guid id);
    }
}
