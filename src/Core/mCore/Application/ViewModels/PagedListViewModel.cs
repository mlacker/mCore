using System;
using System.Collections;
using System.Collections.Generic;

namespace mCore.Application.ViewModels
{
    public class PagedListViewModel<T> : IEnumerable<T>
        where T : IEntityViewModel
    {
        public PagedListViewModel(IEnumerable<T> items, long total)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Total = total;
        }

        public IEnumerable<T> Items { get; private set; }

        public long Total { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
