using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mCore.Services.DataCenter.Core.Expression;
using mCore.Services.DataCenter.Core.Schema;

namespace mCore.Services.DataCenter.Core.Statement.Update
{
    public class Update : IStatement
    {
        public Update(IEnumerable<Column> columns, IEnumerable<IExpression> expressions)
        {
            if (columns.Count() != expressions.Count())
            {
                throw new ArgumentException(
                    $"The count of {nameof(columns)} and {nameof(expressions)} must be equals.");
            }
        }
        public Table Table { get; private set; }

        public IEnumerable<Column> Columns { get; private set; }

        public IEnumerable<IExpression> Expressions { get; private set; }

        public IExpression Where { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"UPDATE {Table} SET ");

            sb.Append(string.Join(", ", Columns.Select((c, i) => $"{c} = {Expressions.ElementAt(i)}")));

            sb.Append($" WHERE {Where}");

            return sb.ToString();
        }
    }
}
