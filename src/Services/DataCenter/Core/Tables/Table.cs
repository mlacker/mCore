using System.Collections.Generic;
using mCore.Domain.Entities;

namespace mCore.Services.DataCenter.Core.Tables
{
    public abstract class Table : Entity, IAggregateRoot
    {
        protected Table()
        {
            Columns = new List<Column>();
            Relations = new List<Relation>();
        }

        public string Name { get; private set; }

        public string TableName { get; private set; }

        public ICollection<Column> Columns { get; private set; }

        public ICollection<Relation> Relations { get; private set; }
    }
}
