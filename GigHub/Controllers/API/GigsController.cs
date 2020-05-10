using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext context;

        public GigsController()
        {
            context = new ApplicationDbContext();
        }


        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = context.Gigs.SingleOrDefault(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
                return NotFound();

            gig.IsCancelled = true;
            context.SaveChanges();
            return Ok();
        }
    }
}
