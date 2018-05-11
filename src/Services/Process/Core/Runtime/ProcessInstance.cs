using System;
using System.Collections.Generic;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Core.Runtime
{
    public class ProcessInstance : Entity, IAggregateRoot
    {
        protected ProcessInstance()
        {
            Activities = new List<ActivityInstance>();
        }

        public ProcessInstance(Guid processDefinitionId, Guid startUserId, ActivityDefinition initial, string businessKey) : this()
        {
            ProcessDefinitionId = processDefinitionId;
            StartUserId = startUserId;
            BusinessKey = businessKey;

            StartActivity = initial;
            Activity = initial;

            StartTime = DateTime.Now;
            Status = InstanceStatusEnum.Created;
        }

        public Guid ProcessDefinitionId { get; private set; }

        public string Name { get; private set; }

        public string BusinessKey { get; private set; }

        public Guid StartUserId { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime? EndTime { get; private set; }

        public TimeSpan? Duration { get; private set; }

        public ActivityDefinition Activity { get; private set; }

        public ActivityDefinition StartActivity { get; private set; }

        public ActivityDefinition EndActivity { get; private set; }

        public ICollection<ActivityInstance> Activities { get; private set; }

        public InstanceStatusEnum Status { get; private set; }

        public void Start()
        {
            // set starting execution

            Status = InstanceStatusEnum.Running;

            // ProcessStartedEvent
        }

        public void Take(Transition transition, bool fireActivityCompletionEvent = true)
        {
            if (fireActivityCompletionEvent)
            {
                // FireActivityCompletionEvent
            }

            Activity = transition.Destination;

            // perform transition_notify_listener_end
        }

        public ActivityInstance CreateStartedActivityInstance()
        {
            var activityInstance = CreateActivityInstance(StartActivity);

            Activities.Add(activityInstance);

            return activityInstance;
        }

        public ActivityInstance CreateCurrentActivityInstance()
        {
            var activityInstance = CreateActivityInstance(Activity);

            Activities.Add(activityInstance);

            return activityInstance;
        }

        private ActivityInstance CreateActivityInstance(ActivityDefinition activityDefinition)
        {
            return activityDefinition.CreateActivityInstance(this);
        }
    }

    public enum InstanceStatusEnum
    {
        Created,
        Running,
        Stopped,
        Aborted
    }
}
