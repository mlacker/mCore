using System;
using System.Collections.Generic;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Core.Runtime
{
    public class Exexution : Entity
    {
        protected Exexution()
        {
            IsActive = true;
            Executions = new List<Exexution>();
        }

        public string Name { get; private set; }

        public ProcessInstance ProcessInstance { get; private set; }

        public Exexution Parent { get; private set; }

        public ICollection<Exexution> Executions { get; private set; }

        public ActivityDefinition ActivityDefinition { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsEnded { get; private set; }

        public bool IsConcurrent { get; private set; }

        public Exexution CreateExecution()
        {
            var createdExecution = new Exexution()
            {
                Parent = this,
                ProcessInstance = ProcessInstance,
                ActivityDefinition = ActivityDefinition,
            };

            Executions.Add(createdExecution);

            // Event: entity created

            return createdExecution;
        }

        public void End()
        {
            IsActive = false;
            IsEnded = true;

            // perform activity_end

            throw new NotImplementedException();
        }

        public void Take(Transition transition, bool fireActivityCompletionEvent = true)
        {
            if (fireActivityCompletionEvent)
            {
                // FireActivityCompletionEvent
            }

            ActivityDefinition = transition.Source;

            // perform transition_notify_listener_end
        }
    }
}