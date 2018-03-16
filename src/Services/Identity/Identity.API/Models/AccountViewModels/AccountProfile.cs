using AutoMapper;

namespace mCore.Services.Identity.API.Models.AccountViewModels
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(d => d.Email, opts => opts.MapFrom(s => s.Username));
        }
    }
}
