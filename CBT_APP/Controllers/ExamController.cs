using CBT_APP.Models;
using CBT_APP.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CBT_APP.Controllers
{
    public class ExamController : Controller
    {
        private QuestionServices services;
        public ExamController()
        {
            services = new QuestionServices();
        }
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetExamQuestions(string id)
        {
            var result = services.GenerateQuestion(id);
            if (result != null)
            {
                var serializedResult = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = 1,
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                });
                var deserializedResult = JsonConvert.DeserializeObject<Dictionary<string, List<Question>>>(serializedResult);
                return Json(new {success= true, data =  deserializedResult}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false}, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<ActionResult> MyExam()
        {
            Dictionary<int, string> result = await services.GetRegisteredCourses(User.Identity.GetUserId());
            if(result != null)
            {
            return View(result);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        
        public async Task<ActionResult> MyExam(FormCollection collection)
        {
            var examqstAns = Request.Form["exam"];
            var result = await services.SubmitExamAnswers(examqstAns);
            if (result )
            {
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("MyExam");
        }


    }
}