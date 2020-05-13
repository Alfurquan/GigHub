using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        List<UserNotification> GetUnreadNotifications(string userId);
        IEnumerable<Notification> GetUserNotifications(string userId);
    }
}