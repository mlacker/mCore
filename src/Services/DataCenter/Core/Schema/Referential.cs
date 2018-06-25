using System;
using mCore.Domain.Entities;

namespace mCore.Services.DataCenter.Core.Schema
{
    public class Referential : Entity
    {
        public Referential(Table table, Column column, Table principalTable, Column principalColumn)
        {
            TableId = table.Id;
            ColumnId = column.Id;
            PrincipalTableId = principalTable.Id;
            PrincipalColumnId = principalColumn.Id;
        }

        protected Referential()
        {
        }

        public Guid TableId { get; private set; }

        public Guid ColumnId { get; private set; }

        public Guid PrincipalTableId { get; private set; }

        public Guid PrincipalColumnId { get; private set; }
    }
}
