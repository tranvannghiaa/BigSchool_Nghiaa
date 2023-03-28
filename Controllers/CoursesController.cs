using BigSchool_Nghia.Models;
using BigSchool_Nghia.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace BigSchool_Nghia.Controllers
{
    public class CoursesController : Controller
        
    {
        
        // GET: Courses
        private readonly ApplicationDbContext _context;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]

        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _context.Categories.ToList()

            };

            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place,

            };
            _context.Courses.Add(course); _context.SaveChanges();
            

            return RedirectToAction("Index", "Home");
        }
    }
}