using System;
using System.ComponentModel.DataAnnotations;
using mCore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace mCore.Services.IdentityServer.Models
{
    public class ApplicationUser : IdentityUser, ISoftDelete
    {
        public const string DEFAULT_PASSWORD = "s123456";

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public DateTime LastLoginTime { get; private set; }

        public bool IsDeleted { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Substring(1, 16);
        }
    }
}
