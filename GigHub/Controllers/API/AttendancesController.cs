using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.Controllers.API
{


    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly UnitOfWork unitOfWork;

        public AttendancesController()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
        }


        [HttpPost]
        public IHttpActionResult Attend([FromBody] AttendenceDto attendence)
        {
            var userId = User.Identity.GetUserId();
            var exists = unitOfWork.Attendances.GetAttendance(attendence.GigId, userId) != null;

            if (exists)
                return BadRequest("Attendance already exists");

            var attendance = new Attendance
            {
                GigId = attendence.GigId,
                AttendeeId = userId
            };
            unitOfWork.Attendances.AddAttendance(attendance);
            unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendence(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();


            unitOfWork.Attendances.RemoveAttendance(attendance);
            unitOfWork.Complete();

            return Ok(id);

        }
    }
}
