using System.Data.Entity;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>().HasRequired(a => a.Gig).WithMany(a => a.Attendances).WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Followers).WithRequired(u => u.Followee).WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Followees).WithRequired(u => u.Follower).WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>().HasRequired(n => n.User).WithMany(a => a.UserNotifications).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}