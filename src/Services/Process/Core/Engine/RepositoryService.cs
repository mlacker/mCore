using System;
using System.Collections.Generic;
using mCore.Services.Process.Core.Validation;

namespace mCore.Services.Process.Core.Engine
{
    public abstract class RepositoryService
    {
        public abstract void ActivateProcessDefinition(string processDefinitionId, bool activateProcessInstances = false, DateTime? activationDate = null);

        public abstract void AddCandidateStarterGroup(string processDefinitionId, string groupId);

        public abstract void AddCandidateStarterUser(string processDefinitionId, string userId);

        public abstract DeploymentBuilder CreateDeployment();

        public abstract void RemoveCandidateStarterGroup(string processDefinitionId, string groupId);

        public abstract void RemoveCandidateStarterUser(string processDefinitionId, string userId);

        public abstract void DeleteDeployment(string deploymentId, bool cascade = false);

        public abstract IEnumerable<ValidationError> ValidateProcess(Definition.Process process);
    }

    public class DeploymentBuilder
    {
    }
}
