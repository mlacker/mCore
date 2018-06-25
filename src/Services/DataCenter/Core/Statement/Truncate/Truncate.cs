using mCore.Services.DataCenter.Core.Schema;

namespace mCore.Services.DataCenter.Core.Statement.Truncate
{
    public class Truncate : IStatement
    {
        public Table Table { get; private set; }

        public override string ToString()
        {
            return $"TRUNCATE TABLE {Table}";
        }
    }
}
