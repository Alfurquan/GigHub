using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return context.Attendances
              .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
              .ToList();
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return context.Attendances.SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }

        public void AddAttendance(Attendance attendance)
        {
            context.Attendances.Add(attendance);
        }

        public void RemoveAttendance(Attendance attendance)
        {
            context.Attendances.Remove(attendance);
        }

    }
}