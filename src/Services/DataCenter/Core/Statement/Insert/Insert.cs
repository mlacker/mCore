using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mCore.Services.DataCenter.Core.Expression.Operators.Relational;
using mCore.Services.DataCenter.Core.Schema;

namespace mCore.Services.DataCenter.Core.Statement.Insert
{
    public class Insert : IStatement
    {
        public Table Table { get; private set; }

        public IEnumerable<Column> Columns { get; private set; }

        public IItemList ItemList { get; private set; }

        public Select.Select Select { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"INSERT INTO {Table} ");

            if (Columns != null && Columns.Count() > 0)
            {
                sb.Append($"({string.Join(", ", Columns)}) ");
            }

            if (ItemList != null)
            {
                sb.Append($"VALUES {ItemList}");
            }
            else if (Select != null)
            {
                sb.Append(Select);
            }
            else
            {
                throw new ArgumentNullException("Property Select or ItemList cannot be null.");
            }

            return sb.ToString();
        }
    }
}
