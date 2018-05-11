using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Core.Runtime
{
    public abstract class ActivityInstance : Entity, IAggregateRoot
    {
        protected ActivityInstance()
        {
            StartTime = DateTime.Now;
            Status = ActivityStatusEnum.Running;
        }

        protected ActivityInstance(ProcessInstance processInstance, ActivityDefinition activityDefinition) : this()
        {
            ProcessInstance = processInstance;
            ActivityDefinition = activityDefinition;
        }

        public ProcessInstance ProcessInstance { get; private set; }

        public ActivityDefinition ActivityDefinition { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime? EndTime { get; private set; }

        public TimeSpan? Duration { get; private set; }

        public ActivityStatusEnum Status { get; set; }
    }

    public enum ActivityStatusEnum
    {
        Created,
        Running,
        Completed,
        Deleted
    }
}
