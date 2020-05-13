using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IGigRepository Gigs { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IAttendanceRepository Attendances { get; private set; }

        public IFollowingRepository Followings { get; private set; }

        public INotificationRepository Notifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Notifications = new NotificationRepository(context);
        }

        public void Complete()
        {
            context.SaveChanges();
        }
    }
}