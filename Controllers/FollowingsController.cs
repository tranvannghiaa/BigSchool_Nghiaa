using BigSchool_Nghia.DTOs;
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
   
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _Context;
        public FollowingsController()
        {
            _Context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId =User.Identity.GetUserId();
            if (_Context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            {
                return BadRequest("Following already exists");
            }
            var folowing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            _Context.Followings.Add(folowing);
            _Context.SaveChanges();
            return Ok();
        }
    }
}
