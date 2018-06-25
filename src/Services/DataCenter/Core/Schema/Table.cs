using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mCore.Domain.Entities;
using mCore.Services.DataCenter.Core.Expression;
using mCore.Services.DataCenter.Core.Statement.Select;

namespace mCore.Services.DataCenter.Core.Schema
{
    public class Table : Entity, IAggregateRoot, IFromItem
    {
        public const string TABLE_PREFIX = "archived_";
        public Table(string name)
        {
            Name = name;

            Setup();
        }

        protected Table()
        {
        }

        public string Name { get; private set; }

        public string TableName { get; private set; }

        public Alias Alias { get; private set; }

        public ICollection<Column> Columns { get; private set; } = new List<Column>();

        public ICollection<Referential> References { get; private set; } = new List<Referential>();

        public ICollection<Index> Indexs { get; private set; } = new List<Index>();

        public Column GetColumn(Guid columnId)
        {
            return Columns.SingleOrDefault(m => m.Id == columnId);
        }

        private Column GetColumn(string columnName)
        {
            return Columns.FirstOrDefault(m => m.ColumnName == columnName);
        }

        public Column GetPrimaryKey()
        {
            return Columns.SingleOrDefault(m => m.IsPrimaryKey) ??
                throw new InvalidOperationException("primary key is not exists.");
        }

        public Column GetDeletedFlag()
        {
            return GetColumn("IsDelete") ??
                throw new InvalidOperationException("deleted flag is not exists.");
        }

        public void AddReference(Table principalTable)
        {
            // create column for reference
            var referenceColumn = new Column($"{principalTable.Name}标识", ColumnTypeEnum.Identity);

            // create reference
            var reference = new Referential(
                this, referenceColumn, principalTable, principalTable.GetPrimaryKey());

            // create index for reference
            var index = new Index(this, referenceColumn);

            Columns.Add(referenceColumn);
            References.Add(reference);
            Indexs.Add(index);
        }

        private void Setup()
        {
            Columns.Add(Column.CreatePrimaryKey());
            Columns.Add(Column.CreateDeletedFlag());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Name);

            if (null != Alias)
            {
                sb.Append(Alias);
            }

            return sb.ToString();
        }
    }
}
