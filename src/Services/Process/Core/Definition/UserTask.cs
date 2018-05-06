using System.Collections.Generic;
using mCore.Services.Process.Core.Expressions;

namespace mCore.Services.Process.Core.Definition
{
    public class UserTask : ActivityDefinition
    {
        protected UserTask() : base()
        {
            Actions = new List<ActionDefinition>();
        }

        public Expression<string> NameExpression { get; private set; }

        public ICollection<ActionDefinition> Actions { get; private set; }
    }
}