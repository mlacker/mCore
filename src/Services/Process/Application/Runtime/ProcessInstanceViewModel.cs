using System;
using mCore.Application.ViewModels;

namespace mCore.Services.Process.Application.Runtime
{
    public class ProcessInstanceViewModel : IEntityViewModel
    {
        public Guid? Id { get; private set; }
    }
}