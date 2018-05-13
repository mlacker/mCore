using System;
using System.Collections;
using System.Collections.Generic;

namespace mCore.Application.ViewModels
{
    public class PagedListViewModel<T>
    {
        public PagedListViewModel(IEnumerable<T> items, long total)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Total = total;
        }

        public IEnumerable<T> Items { get; private set; }

        public long Total { get; private set; }
    }
}
