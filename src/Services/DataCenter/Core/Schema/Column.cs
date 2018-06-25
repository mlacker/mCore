using System.Text;
using mCore.Domain.Entities;
using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Schema
{
    public class Column : Entity, IExpression
    {
        public const string COLUMN_PREFIX = "f_";

        public Column(string name, ColumnTypeEnum columnType)
        {
            Name = name;
            ColumnType = columnType;
        }

        protected Column()
        {
        }

        public string Name { get; private set; }

        public string ColumnName { get; private set; }

        public ColumnTypeEnum ColumnType { get; private set; }

        public int HasMaxLength { get; private set; }

        public bool IsPrimaryKey { get; private set; }

        public bool IsRequired { get; internal set; }

        public bool IsUnicode { get; private set; }

        public virtual Table Table { get; private set; }

        public static Column CreatePrimaryKey()
        {
            return new Column("标识", ColumnTypeEnum.Identity)
            {
                ColumnName = "Id",
                IsPrimaryKey = true,
                IsRequired = true
            };
        }

        public static Column CreateDeletedFlag()
        {
            return new Column("是否删除", ColumnTypeEnum.Boolean)
            {
                ColumnName = "IsDelete",
                IsRequired = true
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (null != Table)
            {
                if (null != Table.Alias)
                {
                    sb.Append(Table.Alias.Name);
                }
                else
                {
                    sb.Append(Table);
                }

                sb.Append(".");
            }

            sb.Append(Name);

            return sb.ToString();
        }
    }

    public enum ColumnTypeEnum
    {
        Boolean = 1,
        Integer = 2,
        Long = 3,
        Decimal = 6,
        String = 8,
        Date = 10,
        Time = 11,
        DateTime = 12,
        Identity = 16,
        List = 18
    }
}
