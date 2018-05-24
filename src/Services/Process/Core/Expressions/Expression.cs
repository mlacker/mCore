namespace mCore.Services.Process.Core.Expressions
{
    public interface Expression<TResult>
    {
        TResult GetValue();
    }

    public class ConstExpression<TResult> : Expression<TResult>
    {
        private readonly TResult value;

        public ConstExpression(TResult value)
        {
            this.value = value;
        }

        public TResult GetValue()
        {
            return value;
        }
    }
}