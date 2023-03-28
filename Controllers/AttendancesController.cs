using BigSchool_Nghia.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool_Nghia.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;
        public AttendancesController() {
            _context = new ApplicationDbContext();

        }
        [HttpPost]
        public IHttpActionResult Attend(Attendance attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
                return BadRequest("The Attendance already exists");
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

    }
}
