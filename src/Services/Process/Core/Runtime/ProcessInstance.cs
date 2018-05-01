using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Identity;

namespace mCore.Services.Process.Core.Runtime
{
    public class ProcessInstance : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string BusinessKey { get; private set; }

        public Definition.Process Process { get; private set; }

        public int ProcessDefinitionVersion { get; private set; }

        public User StartUser { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public Activity StartActivity { get; private set; }

        public Activity EndActivity { get; private set; }

        public InstanceStatusEnum Status { get; private set; }
    }

    public enum InstanceStatusEnum
    {
        Unstarted,
        Running,
        Stopped,
        Aborted
    }
}
