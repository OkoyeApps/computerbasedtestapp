using CBT_APP.Services;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CBT_APP.Controllers
{
    public class HomeController : Controller
    {
        private QuestionServices service;
        private AdminServices adminService;
        public HomeController()
        {
            service = new QuestionServices();
            adminService = new AdminServices();
        }
        public ActionResult Index()
        {
            return View();
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

        public async Task<ActionResult> RegisterCourse()
        {
            var isRegistered = await service.hasRegisteredForExam(User.Identity.GetUserId());
            if(!isRegistered) return View(await adminService.AllCourses(true));
            else
            {
                ViewBag.error = "You have already registered for this exam";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCourse(string courses)
        {
            var result = await service.RegisterCourse(courses);
            if (result)
            {
                return RedirectToAction("RegisterCourse");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}