namespace mCore.Services.Identity.API.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required]
        public string Username { get; private set; }

        [Required]
        public string Password { get; private set; }

        public bool RememberMe { get; private set; }
    }
}
