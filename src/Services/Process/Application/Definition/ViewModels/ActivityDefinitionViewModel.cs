using System;
using System.ComponentModel.DataAnnotations;
using mCore.Application.ViewModels;

namespace mCore.Services.Process.Application.Definition.ViewModels
{
    public class ActivityDefinitionViewModel : IEntityViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}