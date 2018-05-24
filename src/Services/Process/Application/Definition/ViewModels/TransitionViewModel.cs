using System;
using mCore.Application.ViewModels;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Application.Definition.ViewModels
{
    public class TransitionViewModel : IEntityViewModel
    {
        public Guid? Id { get; set; }

        public Guid SourceId { get; set; }

        public Guid DestinationId { get; set; }

        public TransitionTypeEnum TransitionType { get; set; }
    }
}