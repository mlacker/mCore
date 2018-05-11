using System;
using System.Collections.Generic;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Core.Runtime
{
    public class Task : ActivityInstance, IAggregateRoot
    {
        public const int DEFAULT_PRIORITY = 50;

        public Task(ProcessInstance processInstance, UserTask task) : base(processInstance, task)
        {
            Priority = DEFAULT_PRIORITY;

            Comments = new List<Comment>();

            TaskDefinition = task;
        }

        protected Task() : base()
        {
            Priority = DEFAULT_PRIORITY;

            Comments = new List<Comment>();
        }

        public string Name { get; private set; }

        public UserTask TaskDefinition { get; private set; }

        public Guid? AssigneeId { get; private set; }

        public DateTime? ClaimTime { get; private set; }

        public int Priority { get; private set; }

        public ICollection<Comment> Comments { get; private set; }

        public void Complete(Guid currentUserId)
        {
            // fireEvent("complete");

            AssigneeId = currentUserId;

            // delete task
            Status = ActivityStatusEnum.Deleted;

            // execution remove task
        }

        public void SetAssignee(Guid userId)
        {
            AssigneeId = userId;

            // fire event assignment

            // record task assignee change
        }
    }
}
