using mCore.Services.Process.Data.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace mCore.Services.Process.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyDefinitionConfiguration();
            builder.ApplyRuntimeConfiguration();
        }
    }
}
