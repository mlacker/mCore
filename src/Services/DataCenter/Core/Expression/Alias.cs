namespace mCore.Services.DataCenter.Core.Expression
{
    public class Alias
    {
        public string Name { get; private set; }

        public override string ToString()
        {
            return $" AS {Name}";
        }
    }
}
