namespace mCore.Services.Expressions
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; private set; }

        public Expression Right { get; private set; }

        public BinaryExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }
}