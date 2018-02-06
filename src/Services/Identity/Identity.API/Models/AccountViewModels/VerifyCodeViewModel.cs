namespace mCore.Services.Identity.API.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; private set; }

        [Required]
        public string Code { get; private set; }

        public bool RememberBrowser { get; private set; }

        public bool RememberMe { get; private set; }
    }
}
