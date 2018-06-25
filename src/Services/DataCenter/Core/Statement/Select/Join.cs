using System;
using System.Collections.Generic;
using mCore.Exceptions;
using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class Join
    {
        public IFromItem RightItem { get; private set; }

        public IExpression OnExpression { get; private set; }

        public override String ToString()
        {
            return $"LEFT OUTER JOIN {RightItem} ON {OnExpression ?? throw new InvalidArgumentAppException(nameof(OnExpression))} \n";
        }
    }
}
