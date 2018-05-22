using System;
using System.Collections.Generic;
using mCore.Services.Process.Core.Expressions;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Core.Definition
{
    public class UserTask : ActivityDefinition
    {
        public UserTask(string name) : base(name)
        {
            Actions = new List<ActionDefinition>();
        }

        protected UserTask() : base()
        {
            Actions = new List<ActionDefinition>();
        }

        public Expression<string> NameExpression { get; private set; }

        public ICollection<ActionDefinition> Actions { get; private set; }

        public override ActivityInstance CreateActivityInstance(ProcessInstance processInstance)
        {
            var task = new Task(processInstance, this);

            return task;
        }
    }
}