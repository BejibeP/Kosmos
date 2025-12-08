using Kosmos.Models;
using Microsoft.EntityFrameworkCore;

namespace Bejibe.Kosmos.Domain.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<TechnologyModel> Technologies { get; set; }
        public DbSet<DroitModel> Droits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //type.GetTypeConfiguration()
            //    .Configure(builder.Entity<Talent>());

            base.OnModelCreating(builder);
        }

    }
}
