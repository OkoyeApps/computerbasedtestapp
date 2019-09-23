using CBT_APP.Models;
using CBT_APP.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CBT_APP.Controllers
{
    public class AdminController : Controller
    {
        private AdminServices service;
        public AdminController()
        {
            service = new AdminServices();
        }
        // GET: List all Courses availble for exams
        public async Task<ActionResult> Index()
        {
            return View(await service.AllCourses());
        }



        [HttpPost]
        public async Task<ActionResult> AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddCourse(new Courses { name = model.Name, code = model.Code });
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = "Could not add course, kindly try again";
            return View(model);
        }

        public async Task<ActionResult> AddQuestion()
        {
            var courses = await service.AllCourses();
            var courseList = Enum.GetValues(typeof(Answers)).Cast<Answers>().Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }).ToList();
            ViewBag.courses = new SelectList(courses.Select(x => new { id = x.Id, x.name }), "id", "name");
            ViewBag.answers = new SelectList(courseList, "Value", "Text");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(Question model)
        {
            var result = await service.AddQuestionsForCourse(model);
            if (result)
            {
                return RedirectToAction("AddQuestion");
            }
            else
            {
                return View(model);
            }
        }

    }
}