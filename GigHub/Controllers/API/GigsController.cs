using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Core.Models;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly UnitOfWork unitOfWork;

        public GigsController()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }


        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            if (gig.IsCancelled)
                return NotFound();

            gig.Cancel();

            unitOfWork.Complete();

            return Ok();
        }
    }
}
