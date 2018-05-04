using System.Collections.Generic;
using mCore.Services.Process.Core.Expressions;

namespace mCore.Services.Process.Core.Definition
{
    public class UserTask : Activity
    {
        public Expression<string> NameExpression { get; private set; }

        public ICollection<Button> Buttons { get; private set; }
    }
}