using mCore.Data.Configurations;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mCore.Services.Process.Data.ModelConfigurations
{
    internal static class RuntimeConfiguration
    {
        internal static void ApplyRuntimeConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ProcessInstanceConfiguration())
                .ApplyConfiguration(new ActivityInstanceConfiguration())
                .ApplyConfiguration(new TaskConfiguration())
                .ApplyConfiguration(new CommentConfiguration());
        }

        private class ProcessInstanceConfiguration : EntityTypeConfiguration<ProcessInstance>
        {
            protected override void Config(EntityTypeBuilder<ProcessInstance> builder)
            {
                builder.Property(m => m.Name).HasMaxLength(200);
                builder.Property(m => m.BusinessKey).HasMaxLength(200);
                // TODO: Reference Property User
                builder.Property(m => m.StartUserId);
                builder.Property(m => m.StartTime);
                builder.Property(m => m.EndTime);
                builder.Property(m => m.Duration);
                builder.Property(m => m.Status);

                builder.HasOne<ProcessDefinition>().WithMany().IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
                builder.HasMany(m => m.Activities).WithOne(m => m.ProcessInstance).IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Ignore(m => m.Activity);
                builder.Ignore(m => m.StartActivity);
                builder.Ignore(m => m.EndActivity);
            }
        }

        private class ActivityInstanceConfiguration : EntityTypeConfiguration<ActivityInstance>
        {
            protected override void Config(EntityTypeBuilder<ActivityInstance> builder)
            {
                builder.Property(m => m.StartTime);
                builder.Property(m => m.EndTime);
                builder.Property(m => m.Duration);
                builder.Property(m => m.Status);

                builder.HasOne(m => m.ActivityDefinition).WithMany().IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }

        private class TaskConfiguration : EntityTypeConfiguration<Task>
        {
            protected override bool HasBaseType => true;

            protected override void Config(EntityTypeBuilder<Task> builder)
            {
                builder.HasBaseType<ActivityInstance>();

                builder.Property(m => m.Name);
                builder.Property(m => m.AssigneeId);
                builder.Property(m => m.ClaimTime);
                builder.Property(m => m.Priority);

                builder.HasMany(m => m.Comments).WithOne().IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Ignore(m => m.TaskDefinition);
            }
        }

        private class CommentConfiguration : EntityTypeConfiguration<Comment>
        {
            protected override void Config(EntityTypeBuilder<Comment> builder)
            {
                builder.Property(m => m.Time);
                builder.Property(m => m.Message);

                builder.HasOne<ActionDefinition>().WithMany()
                    .HasForeignKey(m => m.ActionId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}