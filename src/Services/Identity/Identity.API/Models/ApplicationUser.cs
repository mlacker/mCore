namespace mCore.Services.Identity.API.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; private set; }
    }
}
