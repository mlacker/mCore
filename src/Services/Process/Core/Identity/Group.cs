using mCore.Domain.Entities;

namespace mCore.Services.Process.Core.Identity
{
    public class Group : Entity
    {
        public string Name { get; private set; }

        public string Type { get; private set; }
    }
}
