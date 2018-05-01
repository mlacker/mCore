using mCore.Domain.Entities;
using mCore.Services.Process.Core.Expressions;

namespace mCore.Services.Process.Core.Definition
{
    public enum TransferEnum
    {
        Pass,
        Back,
        Terminate
    }

    public class Connection : Entity
    {
        public Activity SourceActivity { get; private set; }

        public Activity TargetActivity { get; private set; }

        public TransferEnum TransferType { get; private set; }

        public Expression<bool> Condition { get; private set; }
    }
}