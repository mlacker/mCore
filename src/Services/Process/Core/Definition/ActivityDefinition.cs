using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Definition
{
    public abstract class ActivityDefinition : Entity
    {
        protected ActivityDefinition()
        {
        }

        protected ActivityDefinition(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract ActivityInstance CreateActivityInstance(ProcessInstance processInstance);
    }
}