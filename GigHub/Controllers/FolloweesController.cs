using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using GigHub.Core.Models;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext context;
        private UnitOfWork unitOfWork;

        public FolloweesController()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }

        // GET: Followees
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var followees = unitOfWork.Followings.GetFollowings(userId);
            return View(followees);
        }
    }
}