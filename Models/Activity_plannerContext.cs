using Microsoft.EntityFrameworkCore;
 
namespace activity_planner.Models
{
    public class activity_plannerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public activity_plannerContext(DbContextOptions<activity_plannerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }

    }
}
