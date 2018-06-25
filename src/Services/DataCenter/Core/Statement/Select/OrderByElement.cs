using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class OrderByElement
    {
        public IExpression Expression { get; private set; }

        public bool Asc { get; private set; }

        public override string ToString()
        {
            return $"{Expression} {(!Asc ? "DESC" : "ASC")}";
        }
    }
}
