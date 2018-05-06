using System;
using mCore.Application.ViewModels;

namespace mCore.Services.Process.Application.Runtime.ViewModels
{
    public class ProcessInstanceViewModel : IEntityViewModel
    {
        public Guid? Id { get; private set; }
    }
}