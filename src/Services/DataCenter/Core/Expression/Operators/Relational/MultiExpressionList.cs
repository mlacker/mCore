using System;
using System.Collections.Generic;
using System.Linq;

namespace mCore.Services.DataCenter.Core.Expression.Operators.Relational
{
    public class MultiExpressionList : IItemList
    {
        public ICollection<ExpressionList> ExpressionList { get; private set; }

        public void AddExpressionList(ExpressionList expressionListToAdd)
        {
            if (ExpressionList.Count() > 0 &&
                ExpressionList.First().Expressions.Count() != expressionListToAdd.Expressions.Count())
            {
                throw new ArgumentException(nameof(expressionListToAdd),
                    "Different count of parameters.");
            }

            ExpressionList.Add(expressionListToAdd);
        }

        public override string ToString()
        {
            return string.Join(", ", ExpressionList);
        }
    }
}
