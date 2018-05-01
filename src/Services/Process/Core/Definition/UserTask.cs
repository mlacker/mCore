using System.Collections.Generic;

namespace mCore.Services.Process.Core.Definition
{
    public class UserTask : Activity
    {
        public ICollection<Button> Buttons { get; private set; }
    }
}