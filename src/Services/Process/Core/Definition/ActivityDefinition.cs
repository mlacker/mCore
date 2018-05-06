using mCore.Domain.Entities;

namespace mCore.Services.Process.Core.Definition
{
    public abstract class ActivityDefinition : Entity
    {
        public string Name { get; private set; }
    }
}