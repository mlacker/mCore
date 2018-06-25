using System;
using System.Collections.Generic;
using System.Text;
using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class PlainSelect : ISelectBody
    {
        public ICollection<ISelectItem> SelectItems { get; private set; }

        public IFromItem FromItem { get; private set; }

        public ICollection<Join> Joins { get; private set; }

        public IExpression Where { get; private set; }

        public IEnumerable<IExpression> GroupByColumnReferences { get; private set; }

        public IEnumerable<OrderByElement> OrderByElements { get; private set; }

        public Limit Limit { get; private set; }

        public bool UseBrackets { get; private set; }

        public override string ToString()
        {
            var sql = new StringBuilder();

            if (UseBrackets)
            {
                sql.AppendLine("(");
            }

            sql.AppendLine($"SELECT {String.Join(", ", SelectItems)} ");

            if (FromItem != null)
            {
                sql.AppendLine($"FROM {FromItem} ");

                if (Joins != null)
                {
                    foreach (var join in Joins)
                    {
                        sql.AppendLine($"{join} ");
                    }
                }

                if (Where != null)
                {
                    sql.AppendLine($"WHERE {Where} ");
                }

                if (GroupByColumnReferences != null)
                {
                    sql.AppendLine($"GROUP BY {String.Join(", ", GroupByColumnReferences)} ");
                }

                if (OrderByElements != null)
                {
                    sql.AppendLine($"ORDER BY {String.Join(", ", OrderByElements)} ");
                }

                if (Limit != null)
                {
                    sql.AppendLine($"{Limit} ");
                }
            }
            else
            {
                if (Where != null)
                {
                    sql.AppendLine($"WHERE {Where} ");
                }
            }

            if (UseBrackets)
            {
                sql.AppendLine(")");
            }

            return sql.ToString();
        }
    }
}
