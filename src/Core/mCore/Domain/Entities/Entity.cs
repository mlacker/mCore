namespace mCore.Domain.Entities
{
    using System;

    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }

        public static bool operator ==(Entity left, Entity right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Entity left, Entity right)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is Entity
                && ((Entity)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
