using mCore.Domain.Entities;

namespace mCore.Services.DataCenter.Core.Schema
{
    public class Index : Entity
    {
        public Index(Table table, Column referenceColumn)
        {
            Table = table;
            Column = referenceColumn;
        }

        protected Index()
        {
        }

        public Table Table { get; private set; }

        public Column Column { get; private set; }

        public bool Unique { get; private set; }
    }
}