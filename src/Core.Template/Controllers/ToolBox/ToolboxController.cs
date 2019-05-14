using Core.Template.Infrastructure.Idempotency;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Template.Controllers.ToolBox
{
    /// <summary>
    /// 运维工具箱
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("99")]
    public class ToolboxController : ControllerBase
    {
        private readonly IRequestManager _manager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        public ToolboxController(IRequestManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetByAggregateId(Guid aggregateId)
        {
            return Ok(_manager.GetByAggregateId(aggregateId));
        }
    }
}
