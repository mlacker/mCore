using System;
using mCore.Exceptions;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Event;
using mCore.Services.Process.Core.Identity;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Engine
{
    public class RuntimeService
    {
        public void ActivateProcessInstance(string processInstanceId)
        {
            throw new NotImplementedException();
        }

        public void AddEventListener(ActivitiEventHandler @event)
        {
            throw new NotImplementedException();
        }

        public void AddGroupIdentityLink(string processInstanceId, string groupId, IdentityLinkType identityLinkType = IdentityLinkType.Candidate)
        {
            throw new NotImplementedException();
        }

        public void AddUserIdentityLink(string processInstanceId, string userId, IdentityLinkType identityLinkType = IdentityLinkType.Candidate)
        {
            throw new NotImplementedException();
        }

        public void DeleteProcessInstance(string processInstanceId, string deleteReason)
        {
            throw new NotImplementedException();
        }

        public void DispatchEvent(ActivitiEventHandler @event)
        {
            throw new NotImplementedException();
        }

        public ProcessInstance StartProcessInstance(ProcessDefinition processDefinition, Guid currentUserId, string businessKey = null)
        {
            if (processDefinition.IsSuspended)
            {
                throw new InvalidOperationAppException(
                    $"Cannot start process instance. Process definition {processDefinition.Name} (id = {processDefinition.Id}) is suspended.");
            }

            var processInstance = processDefinition.CreateProcessInstance(currentUserId, businessKey);

            var activityInstance = processInstance.CreateStartedActivityInstance();

            processInstance.Start();

            return processInstance;
        }
    }
}
