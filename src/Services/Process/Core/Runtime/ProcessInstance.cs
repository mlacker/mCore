using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Identity;

namespace mCore.Services.Process.Core.Runtime
{
    public class ProcessInstance : Entity, IAggregateRoot
    {
        public ProcessInstance(Activity initial)
        {
            StartActivity = initial;
        }

        public string Name { get; private set; }

        public string BusinessKey { get; set; }

        public Guid ProcessDefinitionId { get; set; }

        public int ProcessDefinitionVersion { get; private set; }

        public Guid StartUserId { get;  set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public Activity Activity { get; set; }

        public Activity StartActivity { get; private set; }

        public Activity EndActivity { get; private set; }

        public InstanceStatusEnum Status { get; private set; }

        public void Start()
        {
            // Event notify.
        }
    }

    public enum InstanceStatusEnum
    {
        Unstarted,
        Running,
        Stopped,
        Aborted
    }
}
