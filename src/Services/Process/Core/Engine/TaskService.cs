using System;
using System.Collections.Generic;
using System.Text;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Engine
{
    public abstract class TaskService
    {
        public abstract Comment AddComment(string taskId, string processInstanceId, string message);

        public abstract void Claim(string taskId, string userId);

        public abstract void Complate<TData>(string taskId, TData data);

        public abstract void DelegateTask(string taskId, string userId);

        public abstract void DeleteComment(string commentId);

        public abstract void DeleteTask(string taskId, string deleteReason);

        public abstract void DeleteTasks(IEnumerable<string> taskIds, string deleteReason = null, bool cascade = false);

        public abstract TData GetData<TData>(string taskId);

        public abstract void SetAssignee(string taskId, string userId);

        public abstract void Unclaim(string taskId);
    }
}
