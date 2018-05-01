using System;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Identity;

namespace mCore.Services.Process.Core.Runtime
{
    public class Comment : Entity
    {
        public DateTime Time { get; private set; }

        public User User { get; private set; }

        public Task Task { get; private set; }

        public ProcessInstance ProcessInstance { get; private set; }

        public Button Action { get; private set; }

        public string Message { get; private set; }
    }
}