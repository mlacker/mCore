namespace mCore.Services.Identity.API.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordViewModel
    {
        [Required]
        public string Username { get; private set; }
    }
}
