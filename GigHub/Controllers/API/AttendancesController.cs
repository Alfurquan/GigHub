using GigHub.Controllers.API.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{


    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext context;

        public AttendancesController()
        {
            context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult Attend([FromBody] AttendenceDto attendence)
        {
            var userId = User.Identity.GetUserId();
            var exists = context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == attendence.GigId);

            if (exists)
                return BadRequest("Attendance already exists");

            var attendance = new Attendance
            {
                GigId = attendence.GigId,
                AttendeeId = userId
            };
            context.Attendances.Add(attendance);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendence(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendence = context.Attendances.Where(a => a.GigId == id && a.AttendeeId == userId).FirstOrDefault();

            if (attendence == null)
                return NotFound();

            context.Attendances.Remove(attendence);
            context.SaveChanges();

            return Ok(id);

        }
    }
}
