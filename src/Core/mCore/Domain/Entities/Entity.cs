namespace mCore.Domain.Entities
{
    using System;

    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
