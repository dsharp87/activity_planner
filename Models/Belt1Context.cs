using Microsoft.EntityFrameworkCore;
 
namespace belt1.Models
{
    public class Belt1Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Belt1Context(DbContextOptions<Belt1Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }

    }
}
