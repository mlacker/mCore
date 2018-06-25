using mCore.Services.DataCenter.Core.Schema;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class AllTableColumns : ISelectItem
    {
        public AllTableColumns(Table table = null)
        {
            Table = table;
        }

        public Table Table { get; private set; }

        public override string ToString()
        {
            return $"{Table}.* ";
        }
    }
}
