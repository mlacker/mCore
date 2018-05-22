using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using mCore.Application.ViewModels;

namespace mCore.Services.Process.Application.Definition.ViewModels
{
    public class ProcessDefinitionViewModel : IEntityViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        public IEnumerable<ActivityDefinitionViewModel> Activities { get; set; }

        public IEnumerable<TransitionViewModel> Transitions { get; set; }

        public int CurrentVersion { get; set; }

        public string CreatedUser { get; set; }

        public string CreatedTime { get; set; }

        public string IsSuspended { get; set; }
    }
}
