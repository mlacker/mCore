namespace mCore.Domain.Entities
{
    using System.Collections.Generic;

    public enum PaginatedFilterOrder
    {
        ASC,
        DESC
    }

    public class PaginatedFilter
    {
        public PaginatedFilter(int? page, int start = 0, int size = 10)
        {
            if (page.HasValue && page.Value > 0)
            {
                start = page.Value * size;
            }

            Start = start;
            End = start + size;
            Size = size;
            Filters = new Dictionary<string, string>();
        }

        public int Start { get; private set; }

        public int End { get; private set; }

        public int Size { get; private set; }

        public string Sort { get; private set; }

        public PaginatedFilterOrder Order { get; private set; }

        public IDictionary<string, string> Filters { get; private set; }

        public bool ContainsKey(string key)
        {
            return Filters.ContainsKey(key) && !string.IsNullOrEmpty(Filters[key]);
        }

        public string Get(string key)
        {
            return Filters[key];
        }

        public void Add(string key, string value)
        {
            Filters.Add(key, value);
        }
    }
}