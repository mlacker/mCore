using System;
using mCore.Domain.Entities;

namespace mCore.Services.DataCenter.Core.Tables
{
    public class Relation : Entity
    {
        public Guid PrimaryTableId { get; private set; }

        public Guid AssociationTableId { get; private set; }

        public Guid LeftKey { get; private set; }

        public Guid RightKey { get; private set; }
    }
}