using mCore.Services.Process.Core.Identity;

namespace mCore.Services.Process.Core.Engine
{
    public abstract class IdentityService
    {
        public abstract User NewUser(string userId);

        public abstract Group NewGroup(string groupId);

        public abstract void CreateMembership(string userId, string groupId);

        public abstract void DeleteMembership(string userId, string groupId);
    }
}
