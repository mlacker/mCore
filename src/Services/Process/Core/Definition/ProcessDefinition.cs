using System;
using System.Collections.Generic;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Expressions;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Definition
{
    public class ProcessDefinition : Entity, IAggregateRoot
    {
        protected ProcessDefinition()
        {
            Version = 1;

            Activities = new List<ActivityDefinition>();
            Transitions = new List<Transition>();
        }

        public string Name { get; private set; }

        public string Category { get; private set; }

        public int Version { get; private set; }

        public Expression<string> InstanceNameExpression { get; private set; }

        public ActivityDefinition InitialActivity { get; private set; }

        public ICollection<ActivityDefinition> Activities { get; private set; }

        public ICollection<Transition> Transitions { get; private set; }

        public bool IsSuspended { get; private set; }

        public ProcessInstance CreateProcessInstance(Guid currentUserId, string businessKey = null)
        {
            if (InitialActivity == null)
            {
                throw new InvalidOperationException("Cannot start process instance, initial activity where the process instance should start is null.");
            }

            var processInstance = new ProcessInstance(Id, currentUserId, InitialActivity, businessKey);

            // Event process create

            return processInstance;
        }
    }
}
