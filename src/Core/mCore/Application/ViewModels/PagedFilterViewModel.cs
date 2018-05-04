using System.Collections.Generic;

namespace mCore.Application.ViewModels
{
    public class PagedFilterViewModel
    {
        public int Start { get; private set; } = 0;

        public int Size { get; private set; } = 10;

        public string Sort { get; private set; }

        public string Order { get; private set; }

        public IDictionary<string, string> Filters { get; private set; }

        public string this[string key]
        {
            get
            {
                return Filters[key];
            }
            set
            {
                Filters[key] = value;
            }
        }

        public bool ContainsKey(string key)
        {
            return Filters.ContainsKey(key);
        }
    }
}
