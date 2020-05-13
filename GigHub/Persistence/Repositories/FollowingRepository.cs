using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext context;

        public FollowingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return context.Followings.SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

        public IEnumerable<Following> GetFollowings(string userId)
        {
            return context.Followings.Where(f => f.FollowerId == userId).Include(f => f.Followee).ToList();
        }

        public void AddFollowing(Following following)
        {
            context.Followings.Add(following);
        }

        public void RemoveFollowing(Following following)
        {
            context.Followings.Remove(following);
        }
    }
}