using GigHub.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController()
        {
            this.context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = context.Gigs
                .Include(g => g.Artist)
                .Where(g => g.DateTime > DateTime.Now);

            return View(upcomingGigs);
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