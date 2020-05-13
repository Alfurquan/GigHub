using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext context;

        public GigRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return context.Gigs
                .Include(g => Enumerable.Select<Attendance, ApplicationUser>(g.Attendances, a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);

        }

        public Gig GetGig(int gigId)
        {
            return context.Gigs.Include(g => g.Genre).Include(g => g.Artist).SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsForAnArtist(string artistId)
        {
            return context.Gigs.Where(g => g.ArtistId == artistId
                && g.DateTime > DateTime.Now
                && !g.IsCancelled)
                .Include(g => g.Genre).ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigs()
        {
            return context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);
        }


        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();
        }

        public void AddGig(Gig gig)
        {
            context.Gigs.Add(gig);
        }

    }
}