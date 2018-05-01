using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Core.Runtime
{
    public class Exexution : Entity
    {
        public string Name { get; private set; }

        public ProcessInstance ProcessInstance { get; private set; }

        public Activity Activity { get; private set; }
    }
}