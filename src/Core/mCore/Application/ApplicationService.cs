namespace mCore.Application
{
    using AutoMapper;
    using mCore.Domain.Uow;
    using mCore.Logging;

    public abstract class ApplicationService
    {
        protected ApplicationService() { }

        protected IMapper Mapper { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        protected ILogger Logger { get; set; }
    }
}