namespace mCore.Services.Expressions
{
    public class ParameterExpression<T> : Expression
    {
        public string Name { get; private set; }

        public ParameterExpression(string name)
        {
            Name = name;
        }
    }
}