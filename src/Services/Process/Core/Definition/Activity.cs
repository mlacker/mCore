using mCore.Domain.Entities;

namespace mCore.Services.Process.Core.Definition
{
    public abstract class Activity : Entity
    {
        public string Name { get; private set; }
    }
}