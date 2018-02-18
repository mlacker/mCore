namespace mCore.Domain.Entities
{
    using System.Collections;
    using System.Collections.Generic;

    public class PaginatedItems<TEntity> : IEnumerable<TEntity>
        where TEntity : class, IEntity
    {
        public PaginatedItems(IEnumerable<TEntity> data, long count)
        {
            this.Data = data;
            this.Count = count;
        }

        public IEnumerable<TEntity> Data { get; private set; }

        public long Count { get; private set; }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}