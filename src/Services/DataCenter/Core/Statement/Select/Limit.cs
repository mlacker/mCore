namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class Limit
    {
        public long Offset { get; private set; }

        public long RowCount { get; private set; }

        public override string ToString()
        {
            return $"LIMIT {Offset}, {RowCount} ";
        }
    }
}
