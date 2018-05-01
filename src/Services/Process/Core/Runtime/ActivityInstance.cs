using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Core.Runtime
{
    public class ActivityInstance : Entity, IAggregateRoot
    {
        public Activity Activity { get; private set; }

        public ProcessInstance ProcessInstance { get; private set; }

        public Exexution Exexution { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public ActivityStatusEnum Status { get; private set; }
    }

    public enum ActivityStatusEnum
    {
        Running,
        Completed,
        Deleted
    }
}
