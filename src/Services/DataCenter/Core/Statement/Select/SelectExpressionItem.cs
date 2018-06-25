using System.Text;
using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class SelectExpressionItem : ISelectItem
    {
        public SelectExpressionItem(IExpression expression = null, Alias alias = null)
        {
            Expression = expression;
            Alias = alias;
        }

        public IExpression Expression { get; private set; }

        public Alias Alias { get; private set; }

        public override string ToString()
        {
            var retval = new StringBuilder();

            retval.Append($"({Expression})");

            if (null != Alias)
            {
                retval.Append(Alias);
            }

            return retval.ToString();
        }
    }
}
