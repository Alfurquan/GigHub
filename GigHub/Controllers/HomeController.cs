using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UnitOfWork unitOfWork;


        public HomeController()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = unitOfWork.Gigs.GetUpcomingGigs();

            if (!string.IsNullOrWhiteSpace(query))
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query)
                    || g.Genre.Name.Contains(query)
                    || g.Venue.Contains(query));

            var userId = User.Identity.GetUserId();


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId),
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}