using System.Data.Entity;
using Twitch10WcfService.DAL.Entities;

namespace Twitch10WcfService.DAL
{
    public class TwitchContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<UserHasBuild> UserHasBuilds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}