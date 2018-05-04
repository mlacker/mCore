using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Identity;

namespace mCore.Services.Process.Core.Runtime
{
    public class ProcessInstance : Entity, IAggregateRoot
    {
        public ProcessInstance(Guid processDefinitionId, Guid startUserId, Activity initial, string businessKey)
        {
            BusinessKey = businessKey;
            ProcessDefinitionId = processDefinitionId;
            StartTime = DateTime.Now;
            StartUserId = startUserId;
            StartActivity = initial;
            Activity = initial;
            Status = InstanceStatusEnum.Running;
            // Executions = new List<Exexution>();
        }

        public string Name { get; private set; }

        public string BusinessKey { get; private set; }

        public Guid ProcessDefinitionId { get; private set; }

        public int ProcessDefinitionVersion { get; private set; }

        public Guid StartUserId { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public Activity Activity { get; private set; }

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
