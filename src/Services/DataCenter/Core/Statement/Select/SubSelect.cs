using System.Text;
using mCore.Services.DataCenter.Core.Expression;
using mCore.Services.DataCenter.Core.Expression.Operators.Relational;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class SubSelect : IFromItem, IExpression, IItemList
    {
        public ISelectBody SelectBody { get; private set; }

        public Alias Alias { get; private set; }

        public override string ToString()
        {
            var retval = new StringBuilder();

            retval.Append($"({SelectBody})");

            if (null != Alias)
            {
                retval.Append(Alias);
            }

            return retval.ToString();
        }
    }
}
