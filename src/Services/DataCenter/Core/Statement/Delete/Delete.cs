using System.Text;
using mCore.Services.DataCenter.Core.Schema;
using mCore.Services.DataCenter.Core.Expression;

namespace mCore.Services.DataCenter.Core.Statement.Delete
{
    public class Delete : IStatement
    {
        public Table Table { get; private set; }

        public IExpression Where { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"DELETE FROM {Table}");

            if (null != Where)
            {
                sb.Append($"WHERE {Where}");
            }

            return sb.ToString();
        }
    }
}
