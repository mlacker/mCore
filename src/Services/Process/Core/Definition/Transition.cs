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
        public Transition(ActivityDefinition source, ActivityDefinition destination, TransitionTypeEnum transitionType = TransitionTypeEnum.Pass, Expression<bool> condition = null) : this()
        {
            Source = source;
            Destination = destination;
            TransitionType = transitionType;
            Condition = condition ?? new ConstExpression<bool>(true);
        }

        protected Transition()
        {

        }

        public ActivityDefinition Source { get; private set; }

        public ActivityDefinition Destination { get; private set; }

        public TransitionTypeEnum TransitionType { get; private set; }

        public Expression<bool> Condition { get; private set; }
    }
}