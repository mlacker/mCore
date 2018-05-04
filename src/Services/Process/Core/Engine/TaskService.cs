using System;
using System.Collections.Generic;
using System.Text;
using mCore.Exceptions;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Engine
{
    public abstract class TaskService
    {
        public void Complete(Task task, Guid currentUserId)
        {
            if (task.Status != ActivityStatusEnum.Running)
            {
                throw new InvalidOperationAppException($"当前任务已被处理.");
            }

            if (task.AssigneeId != currentUserId && task.AssigneeId != null)
            {
                throw new InvalidOperationAppException($"您不是当前任务的处理人.");
            }

            task.Complete(currentUserId);

            // fire event task complete
            
            // delete task

            // Get all out transitions;
            // Check transition, push to transitionsToTake if condition evaluate true.
            // Handle default transition logic (no condition).

            // take(transition)
            // Set activity to current execution.

            // create next task
            // handle some like name expression.
            // handle assigments.

            // fire event task create
        }

        public abstract void Claim(string taskId, string userId);

        public abstract void Unclaim(string taskId);

        public abstract void SetAssignee(string taskId, string userId);

        public abstract void DelegateTask(string taskId, string userId);

        public abstract Comment CreateComment(string taskId, string processInstanceId, string message);

        public abstract void DeleteComment(string commentId);

        public abstract void DeleteTask(string taskId, string deleteReason);

        public abstract void DeleteTasks(IEnumerable<string> taskIds, string deleteReason = null, bool cascade = false);
    }
}
