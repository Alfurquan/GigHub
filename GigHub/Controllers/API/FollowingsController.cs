using GigHub.Controllers.API.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext context;

        public FollowingsController()
        {
            context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var exists = context.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == dto.FolloweeId);
            if (exists)
                return BadRequest("Following already exists");

            var following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };
            context.Followings.Add(following);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
                return NotFound();

            context.Followings.Remove(following);
            context.SaveChanges();

            return Ok();
        }
    }
}
