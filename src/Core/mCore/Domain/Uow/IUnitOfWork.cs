namespace mCore.Domain.Uow
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();

        void Rollback();

        Task RollbackAsync();
    }
}