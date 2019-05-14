using Core.Template.Domain.SeedWork;
using Core.Template.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR
{
    /// <summary>
    /// 
    /// </summary>
    public static class MediatorExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, TemplateContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                  .Entries<IEntity>()
                  .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
