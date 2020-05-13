using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        void AddFollowing(Following following);
        Following GetFollowing(string artistId, string userId);
        IEnumerable<Following> GetFollowings(string userId);
        void RemoveFollowing(Following following);
    }
}