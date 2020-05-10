using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext context;

        public FolloweesController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Followees
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var followees = context.Followings.Where(f => f.FollowerId == userId).Include(f => f.Followee).ToList();
            return View(followees);
        }
    }
}