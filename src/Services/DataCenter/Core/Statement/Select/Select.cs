namespace mCore.Services.DataCenter.Core.Statement.Select
{
    public class Select : IStatement
    {
        public ISelectBody SelectBody { get; private set; }

        public override string ToString()
        {
            return SelectBody.ToString();
        }
    }
}
