using Microsoft.EntityFrameworkCore;

namespace BigBangIII_Api.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Agent> agents { get; set; }
        public DbSet<Billing> billings { get; set; }
        public DbSet<Bookings> bookings { get; set; }
        public DbSet<Images> images { get; set; }
        public DbSet<Packages> packages { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
    }
}
