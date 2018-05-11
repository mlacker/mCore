using System;
using System.Collections.Generic;
using System.Linq;
using mCore.Exceptions;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Engine
{
    public class TaskService
    {
        public Task Complete(Task task, ProcessDefinition processDefinition, Guid currentUserId)
        {
            if (task.Status != ActivityStatusEnum.Running)
            {
                throw new InvalidOperationAppException($"当前任务已被处理.");
            }

            if (task.AssigneeId != currentUserId && task.AssigneeId != null)
            {
                throw new InvalidOperationAppException($"您不是当前任务的处理人.");
            }

            var processInstance = task.ProcessInstance;

            task.Complete(currentUserId);

            // fire event task complete

            // delete task
            task.Status = ActivityStatusEnum.Deleted;

            // Get all out transitions;
            // Check transition, push to transitionsToTake if condition evaluate true.
            // Handle default transition logic (no condition).
            var outTransitions = GetOutTransitions(processDefinition, task.ActivityDefinition);
            var outTransition = outTransitions.First(m => m.Condition.GetValue());

            // take(transition)
            // Set activity to current execution.
            processInstance.Take(outTransition);

            // create next task
            // handle some like name expression.
            // handle assigments.
            var nextTask = (Task)processInstance.CreateCurrentActivityInstance();

            // fire event task create

            return nextTask;
        }

        public IEnumerable<Transition> GetOutTransitions(ProcessDefinition processDefinition, ActivityDefinition activityDefinition)
        {
            return processDefinition.Transitions.Where(m => m.Source == activityDefinition);
        }

        public void Claim(string taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Unclaim(string taskId)
        {
            throw new NotImplementedException();
        }

        public void SetAssignee(string taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public void DelegateTask(string taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public Comment CreateComment(string taskId, string processInstanceId, string message)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(string commentId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(string taskId, string deleteReason)
        {
            throw new NotImplementedException();
        }

        public void DeleteTasks(IEnumerable<string> taskIds, string deleteReason = null, bool cascade = false)
        {
            throw new NotImplementedException();
        }
    }
}
