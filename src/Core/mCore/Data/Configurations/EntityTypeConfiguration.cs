using mCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mCore.Data.Configurations
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        protected virtual bool HasBaseType => false;

        protected virtual bool HasDeleteFilter => true;

        protected abstract void Config(EntityTypeBuilder<TEntity> builder);

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (!HasBaseType)
            {
                builder.HasKey(m => m.Id);
                builder.Property(m => m.Id).ValueGeneratedOnAdd();
            }

            Config(builder);

            if (HasDeleteFilter && typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                builder.HasQueryFilter(m => ((ISoftDelete)m).IsDeleted == false);
            }
        }
    }
}
