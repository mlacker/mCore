namespace mCore.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class PaginatedItems<TEntity> 
        where TEntity : class
    {
        public PaginatedItems(IEnumerable<TEntity> items, long total)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Total = total;
        }

        public IEnumerable<TEntity> Items { get; private set; }

        public long Total { get; private set; }
    }
}