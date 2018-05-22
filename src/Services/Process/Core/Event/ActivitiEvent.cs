using System;

namespace mCore.Services.Process.Core.Event
{
    public delegate void ActivitiEventHandler(object sender, ActivitiEventArgs e);

    public class ActivitiEventArgs : EventArgs
    {
    }
}
