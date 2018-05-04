using mCore.Domain.Entities;

namespace mCore.Services.Process.Core.Expressions
{
    public interface Expression<TResult>
    {
        string ExpressionText { get; }

        TResult GetValue();
    }
}