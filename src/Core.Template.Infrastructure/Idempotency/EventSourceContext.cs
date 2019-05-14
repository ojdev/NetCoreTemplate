using Microsoft.EntityFrameworkCore;

namespace Core.Template.Infrastructure.Idempotency
{
    /// <summary>
    /// 
    /// </summary>
    public class EventSourceContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public EventSourceContext(DbContextOptions<EventSourceContext> options) : base(options)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ClientRequest> ClientRequests { get; set; }

    }
}
