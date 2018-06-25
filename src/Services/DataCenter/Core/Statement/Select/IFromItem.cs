using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    /// <summary>
    /// An item in a select statement. (for example a table or a sub-select)
    /// </summary>
    public interface IFromItem
    {
        Alias Alias { get; }
    }
}
