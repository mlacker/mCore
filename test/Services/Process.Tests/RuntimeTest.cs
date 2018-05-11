using System;
using System.Collections.Generic;
using System.Linq;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Engine;
using mCore.Services.Process.Core.Runtime;
using Xunit;

namespace Process.Tests
{
    public class RuntimeTest
    {
        private readonly RuntimeService runtimeService;
        private readonly TaskService taskService;

        public RuntimeTest()
        {
            runtimeService = new RuntimeService();
            taskService = new TaskService();
        }

        [Fact]
        public void StartProcessInstanceTest()
        {
            var processDefinition = GetProcessDefinition();

            var processInstance = runtimeService.StartProcessInstance(processDefinition, new Guid());

            Assert.Contains(processDefinition.Activities.First(), processInstance.Activities.Select(m => m.ActivityDefinition));
        }

        [Fact]
        public void CompleteTaskTest()
        {
            var processDefinition = GetProcessDefinition();

            var processInstance = runtimeService.StartProcessInstance(processDefinition, new Guid());

            var task = processInstance.Activities.Where(m => m.Status == ActivityStatusEnum.Running).First();

            taskService.Complete((Task)task, processDefinition, new Guid());
        }

        private ProcessDefinition GetProcessDefinition()
        {
            var task1 = new UserTask("Task 1");
            var task2 = new UserTask("Task 2");
            var task3 = new UserTask("Task 3");

            var transition1 = new Transition(task1, task2);
            var transition2 = new Transition(task2, task3);

            var processDefinition = new ProcessDefinition(
                name: "Test",
                category: "Test Category",
                initialActivity: task1
                );

            processDefinition.Activities.Add(task1);
            processDefinition.Activities.Add(task2);
            processDefinition.Activities.Add(task3);

            processDefinition.Transitions.Add(transition1);
            processDefinition.Transitions.Add(transition2);

            return processDefinition;
        }
    }
}
