using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Core.Template.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateContextSeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public Task SeedAsync(TemplateContext context, ILogger<TemplateContextSeed> logger)
        {
            return Task.CompletedTask;
        }
    }
}
