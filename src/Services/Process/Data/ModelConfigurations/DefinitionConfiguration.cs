using mCore.Data.Configurations;
using mCore.Services.Process.Core.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mCore.Services.Process.Data.ModelConfigurations
{
    internal static class DefinitionConfiguration
    {
        internal static void ApplyDefinitionConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ProcessDefinitionConfiguration())
                .ApplyConfiguration(new ActivityDefinitionConfiguration())
                .ApplyConfiguration(new UserTaskConfiguration())
                .ApplyConfiguration(new ActionDefinitionConfiguration())
                .ApplyConfiguration(new TransitionConfiguration());
        }

        private class ProcessDefinitionConfiguration : EntityTypeConfiguration<ProcessDefinition>
        {
            protected override void Config(EntityTypeBuilder<ProcessDefinition> builder)
            {
                builder.Property(m => m.Name).HasMaxLength(200).IsRequired();
                builder.Property(m => m.Category).HasMaxLength(200).IsRequired();
                builder.Property(m => m.Version);
                builder.Property(m => m.IsSuspended);

                builder.HasOne(m => m.InitialActivity).WithOne().IsRequired()
                    .HasForeignKey<ProcessDefinition>("InitialActivityId")
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasMany(m => m.Activities).WithOne().IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(m => m.Transitions).WithOne().IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Ignore(m => m.InstanceNameExpression);
            }
        }

        private class ActivityDefinitionConfiguration : EntityTypeConfiguration<ActivityDefinition>
        {
            protected override void Config(EntityTypeBuilder<ActivityDefinition> builder)
            {
                builder.Property(m => m.Name).HasMaxLength(200).IsRequired();
            }
        }

        private class UserTaskConfiguration : EntityTypeConfiguration<UserTask>
        {
            protected override bool HasBaseType => true;

            protected override void Config(EntityTypeBuilder<UserTask> builder)
            {
                builder.HasBaseType<ActivityDefinition>();

                builder.HasMany(m => m.Actions).WithOne().IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Ignore(m => m.NameExpression);
            }
        }

        private class ActionDefinitionConfiguration : EntityTypeConfiguration<ActionDefinition>
        {
            protected override void Config(EntityTypeBuilder<ActionDefinition> builder)
            {
                builder.Property(m => m.Name).HasMaxLength(200).IsRequired();
            }
        }

        private class TransitionConfiguration : EntityTypeConfiguration<Transition>
        {
            protected override void Config(EntityTypeBuilder<Transition> builder)
            {
                builder.Property(m => m.TransitionType);

                builder.HasOne(m => m.Source).WithMany().IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasOne(m => m.Destination).WithMany().IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                builder.Ignore(m => m.Condition);
            }
        }
    }
}
