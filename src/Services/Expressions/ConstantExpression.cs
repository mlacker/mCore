namespace mCore.Services.Expressions
{
    public class ConstantExpression<T> : Expression
    {
        private T value;

        public ConstantExpression(T value) 
        {
            this.value = value;
        }
    }
}