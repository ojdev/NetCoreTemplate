using Core.Template.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Core.Template.Infrastructure.EntityConfigurations
{
    class EntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TKey> where TKey : struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Ignore(o => o.DomainEvents);
            if (typeof(AuditEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                builder.Property<bool?>("IsDeleted").HasDefaultValue(false);
                builder.Ignore("CreationTime");
                builder.Property<DateTimeOffset>("CreationTime").HasField("_creationTime").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
                builder.Property<DateTimeOffset?>("LastUpdateTime").HasField("_lastUpdateTime");
                builder.Property<DateTimeOffset?>("DeletionTime");
                builder.HasQueryFilter(o => !EF.Property<bool>(o, "IsDeleted"));
            }
            builder.Property<byte[]>("RowVersion").IsRowVersion();
        }
    }
}
