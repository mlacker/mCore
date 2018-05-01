using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Identity;

namespace mCore.Services.Process.Core.Runtime
{
    public class Task : ActivityInstance, IAggregateRoot
    {
        public const int DEFAULT_PRIORITY = 50;

        public string Name { get; private set; }

        public UserTask TaskDefinition { get; private set; }

        public User Assignee { get; private set; }

        public User Delegation { get; private set; }

        public DateTime ClaimTime { get; private set; }

        public int Priority { get; private set; }

        public Comment Comment { get; private set; }
    }
}
