namespace mCore.Services.Identity.API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Substring(1, 16);
        }

        [Required]
        public string Name { get; private set; }

        public DateTime LastLoginTime { get; private set; }

        public bool IsDeleted { get; private set; }
    }
}
