using System.Collections.Generic;

namespace mCore.Application.ViewModels
{
    public class PagedFilterViewModel
    {
        public int Index { get; set; } = 1;

        public int Start { get => Index > 0 ? (Index - 1) * Size : 0; }

        public int Size { get; set; } = 10;

        public string Sort { get; set; }

        public string Order { get; set; }

        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

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
            return Filters.ContainsKey(key) && !string.IsNullOrEmpty(this[key]);
        }
    }
}
