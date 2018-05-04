using mCore.Domain.Entities;
using mCore.Services.Process.Core.Expressions;

namespace mCore.Services.Process.Core.Definition
{
    public enum TransitionTypeEnum
    {
        Pass,
        Back,
        Terminate
    }

    public class Transition : Entity
    {
        public Activity Source { get; private set; }

        public Activity Destination { get; private set; }

        public TransitionTypeEnum TransitionType { get; private set; }

        public Expression<bool> Condition { get; private set; }
    }
}