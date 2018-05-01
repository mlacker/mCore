using System;
using System.Collections.Generic;
using System.Text;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Engine
{
    public abstract class TaskService
    {
        public void Complete(Task task, Guid currentUserId)
        {
            task.Complete(currentUserId);
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
