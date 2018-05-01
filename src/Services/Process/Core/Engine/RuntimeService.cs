using System;
using System.Collections.Generic;
using System.Text;
using mCore.Services.Process.Core.Event;
using mCore.Services.Process.Core.Identity;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Engine
{
    public abstract class RuntimeService
    {
        public abstract void ActivateProcessInstance(string processInstanceId);

        public abstract void AddEventListener(ActivitiEventHandler @event);

        public abstract void AddGroupIdentityLink(string processInstanceId, string groupId, IdentityLinkType identityLinkType = IdentityLinkType.Candidate);

        public abstract void AddUserIdentityLink(string processInstanceId, string userId, IdentityLinkType identityLinkType = IdentityLinkType.Candidate);

        public abstract void DeleteProcessInstance(string processInstanceId, string deleteReason);

        public abstract void DispatchEvent(ActivitiEventHandler @event);

        public abstract ProcessInstance StartProcessInstance(string processDefinitionId);
    }
}
