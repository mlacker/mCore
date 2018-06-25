using System.Collections.Generic;

namespace mCore.Services.DataCenter.Core.Expression.Operators.Relational
{
    /// <summary>
    /// A list of expressions, as in SELECT A FROM TAB WHERE B IN (expr1, expr2, expr3)
    /// </summary>
    public class ExpressionList : IItemList
    {
        public IEnumerable<IExpression> Expressions { get; private set; }

        public override string ToString()
        {
            return $"({string.Join(", ", Expressions)})";
        }
    }
}
