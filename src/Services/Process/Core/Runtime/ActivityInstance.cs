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
            Status = ActivityStatusEnum.Created;
        }

        public ProcessInstance ProcessInstance { get; private set; }

        public ActivityDefinition ActivityDefinition { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime? EndTime { get; private set; }

        public TimeSpan? Duration { get; private set; }

        public ActivityStatusEnum Status { get; protected set; }
    }

    public enum ActivityStatusEnum
    {
        Created,
        Running,
        Completed,
        Deleted
    }
}
