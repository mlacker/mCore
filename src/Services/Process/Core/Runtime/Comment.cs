using System;
using mCore.Domain.Entities;

namespace mCore.Services.Process.Core.Runtime
{
    public class Comment : Entity
    {
        public Guid ActionId { get; private set; }

        public DateTime Time { get; private set; }

        public string Message { get; private set; }
    }
}