using GigHub.AutoMapperConfig;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext context;
        private UnitOfWork unitOfWork;

        public NotificationsController()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = unitOfWork.Notifications.GetUserNotifications(userId);

            return MapperWrapper.Mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notifications);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = unitOfWork.Notifications.GetUnreadNotifications(userId);

            notifications.ForEach(n => n.Read());

            unitOfWork.Complete();

            return Ok();
        }

    }
}
