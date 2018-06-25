namespace mCore.Services.DataCenter.Core.Statement
{
    public interface IStatementVisitor
    {
        void visit(Select.Select stament);
    }
}
