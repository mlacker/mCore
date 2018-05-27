using mCore.Domain.Entities;

namespace mCore.Services.DataCenter.Core.Tables
{
    public abstract class Column : Entity
    {
        protected Column()
        {
        }

        public string Name { get; private set; }

        public string DataType { get; private set; }

        public bool Required { get; private set; }

        public bool PrimaryKey { get; private set; }

        public string ColumnName { get; private set; }

        public string ColumnType { get; private set; }
    }

    public enum DataType
    {
        Real,

        Bit,
        Int,
        Bigint,
        Double,
        Decimal,
        Date,
        Time,
        Timestamp,
        Datetime,
        Char,
        Varchar,
        Blob,
        Longblob,
        Tinytext,
        Text,
        Mediumtext,
        Longtext,
        Enum,
        Set,
        Json
    }
}