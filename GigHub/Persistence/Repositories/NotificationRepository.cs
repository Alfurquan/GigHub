using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext context;

        public NotificationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Notification> GetUserNotifications(string userId)
        {
            return context.UserNotifications
                    .Where(u => u.UserId == userId && !u.IsRead)
                    .Select(u => u.Notification)
                    .Include(n => n.Gig.Artist).ToList();
        }

        public List<UserNotification> GetUnreadNotifications(string userId)
        {
            return context.UserNotifications.Where(u => u.UserId == userId && !u.IsRead).ToList();
        }
    }
}