using System;
using mCore.Application.ViewModels;

namespace mCore.Services.Process.Application.Runtime.ViewModels
{
    public class DoingTaskViewModel : IEntityViewModel
    {
        public Guid? Id { get; private set; }

        public string ProcessInstanceId { get; private set; }

        public string Name { get; private set; }

        public string ProcessName { get; private set; }

        public string TaskName { get; private set; }

        public string Assignee { get; private set; }

        public string Started { get; private set; }

        public DateTime StartedTime { get; private set; }
    }
}