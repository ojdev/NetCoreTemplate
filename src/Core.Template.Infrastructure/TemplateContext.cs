using Core.Template.Domain.AggregatesModel;
using Core.Template.Domain.Exceptions;
using Core.Template.Domain.SeedWork;
using Core.Template.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Template.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateContext : DbContext, IUnitOfWork
    {
        #region Property
        //private static readonly AsyncLock _locked = new AsyncLock();
        private readonly ILogger<TemplateContext> _logger;
        private readonly IMediator _mediator;
        #endregion
        /// <summary>
        /// 演示用
        /// </summary>
        public DbSet<UserDemo> UserDemos { get; set; }
        /// <summary>
        /// 演示用
        /// </summary>
        public DbSet<Message> Messages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public TemplateContext(DbContextOptions<TemplateContext> options, IMediator mediator, ILogger<TemplateContext> logger) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<UserDemo, Guid>());
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Message, Guid>());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries().Where(t => t.GetType() == typeof(AuditEntity<>) && t.State == EntityState.Deleted || t.State == EntityState.Modified))
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        {
                            entry.CurrentValues["DeletionTime"] = DateTimeOffset.Now;
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.State = EntityState.Modified;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entry.CurrentValues["LastUpdateTime"] = DateTimeOffset.Now;
                            break;
                        }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            bool saveResult = false;
            try
            {
                await base.SaveChangesAsync();
                saveResult = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
                throw new DomainException("保存数据时产生错误。", ex);
            }
            finally
            {
                if (saveResult)
                {
                    await _mediator.DispatchDomainEventsAsync(this);
                }
            }
            return saveResult;
        }
    }
}
