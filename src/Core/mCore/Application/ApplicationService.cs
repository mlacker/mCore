namespace mCore.Application
{
    using mCore.Domain.Uow;
    using mCore.Logging;

    public abstract class ApplicationService
    {
        protected ApplicationService() {}
        
        protected IUnitOfWork UnitOfWork { get; set; }

        protected ILogger Logger { get; set; }
    }
}