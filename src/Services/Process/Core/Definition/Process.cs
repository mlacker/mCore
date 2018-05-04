using System;
using System.Collections.Generic;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Expressions;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Definition
{
    public class Process : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Category { get; private set; }

        public byte Version { get; private set; }

        public Expression<string> InstanceNameExpression { get; private set; }

        public ICollection<Activity> Activities { get; private set; }

        public Activity Initial { get; private set; }

        public ICollection<Transition> Transitions { get; private set; }

        public bool IsSuspended { get; private set; }

        public ProcessInstance CreateProcessInstance(Guid currentUserId, string businessKey = null)
        {
            if (Initial == null)
            {
                throw new InvalidOperationException("Cannot start process instance, initial activity where the process instance should start is null.");
            }

            var processInstance = new ProcessInstance(Id, currentUserId, Initial, businessKey);

            // TODO: Event dispatch

            return processInstance;
        }
    }
}
