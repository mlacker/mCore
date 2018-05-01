using System.Collections.Generic;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Expressions;

namespace mCore.Services.Process.Core.Definition
{
    public class Process : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Category { get; private set; }

        public byte Version { get; private set; }

        public Expression<string> InstanceTitle { get; private set; }

        public ICollection<Activity> Activities { get; private set; }

        public Activity StartedActivity { get; private set; }

        public ICollection<Connection> Connections { get; private set; }
    }
}
