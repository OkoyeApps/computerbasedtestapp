using CBT_APP.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CBT_APP.Services
{
    public class QuestionServices
    {
        private ApplicationDbContext db;

        public QuestionServices()
        {
            db = new ApplicationDbContext();
        }

        public async Task<bool> hasRegisteredForExam(string userId)
        {
            var RegisteredStudent = await db.CourseRegistration.FirstOrDefaultAsync(x => x.userId == userId);
            if (RegisteredStudent != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> RegisterCourse(string coursesId)
        {


            try
            {

                var courses = coursesId.Split(',');
                var courseRegistration = new CourseRegistration
                {
                    course1Id = int.Parse(courses[0]),
                    course2Id = int.Parse(courses[1]),
                    course3Id = int.Parse(courses[2]),
                    course4Id = int.Parse(courses[3]),
                    userId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.CourseRegistration.Add(courseRegistration);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /*
         *The int in the dictionary is for the Id of the course
         */
        public Dictionary<int, List<Question>> GenerateQuestion(string userId)
        {
            var coursesGotten = db.CourseRegistration.FirstOrDefault(x => x.userId == userId);
            if (coursesGotten != null)
            {

                var CourseAndQuestions = GenerateRandomQuestion(new int[] {  coursesGotten.course1Id, coursesGotten.course2Id,
                coursesGotten.course3Id, coursesGotten.course4Id  });
                return CourseAndQuestions;
            }
            return null;
        }

        private Dictionary<int, List<Question>> GenerateRandomQuestion(int[] courseId)
        {
            var CoursesDictionary = new Dictionary<int, List<Question>>();
            for (int i = 0; i < courseId.Length; i++)
            {
                int coursedetail = courseId[i];
                var currentCourse = db.Course.Where(x => x.Id == coursedetail).Include("Questions").FirstOrDefault();
                CoursesDictionary.Add(currentCourse.Id, currentCourse.Questions);
            }
            return CoursesDictionary;
        }

        /*
         *The three values of the tuples stands for questionId, correctAns for Question
         */
        public async Task<bool> SubmitExamAnswers(string qsts)
        {
            try
            {
                var questionArrays = qsts.Split(',');
                if (questionArrays.Length > 0)
                {
                    var AnsweredQuestions = new List<CourseExam>();
                    foreach (var question in questionArrays)
                    {
                        try
                        {
                            var splitedqstAns = question.Split(':'); //this splits the result by : and it should contain three arrays of courseId, questionId, Ans
                            if (splitedqstAns.Length > 0)
                            {
                                var qstionId = int.Parse(splitedqstAns[1]);
                                var questionexist = await db.Question.FirstOrDefaultAsync(x => x.id == qstionId);
                                if (questionexist != null)
                                {
                                    var courseExam = new CourseExam()
                                    {
                                        correctAns = questionexist.correctAnswer,
                                        courseId = questionexist.courseId,
                                        score = questionexist.weight,
                                        studentAns = int.Parse(splitedqstAns[2]),
                                        userId = HttpContext.Current.User.Identity.GetUserId()
                                    };
                                    AnsweredQuestions.Add(courseExam);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                    db.CourseExam.AddRange(AnsweredQuestions);
                    await db.SaveChangesAsync();
                    await calculateExamResult(HttpContext.Current.User.Identity.GetUserId());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task calculateExamResult(string userId)
        {
            try
            {
                var coursesRegistered = await db.CourseRegistration.FirstOrDefaultAsync(x => x.userId == userId);
                if (coursesRegistered != null)
                {
                    var allResult = new List<Result>();
                    var arrayOfCourses = new int[] { coursesRegistered.course1Id, coursesRegistered.course2Id, coursesRegistered.course3Id, coursesRegistered.course4Id };
                    foreach (var course in arrayOfCourses)
                    {
                        var studentcount = db.CourseRegistration.Select(x => x.userId).Distinct().Count();
                        var courseQuestions = await db.CourseExam.Where(x => x.userId == userId && x.courseId == course).ToListAsync();
                        var sum = courseQuestions.Sum(x => calculateExamSum(x));
                        var average = courseQuestions.Average(x => calculateExamSum(x));
                        var percentage = average * 100;
                        var classpercentage = (average / studentcount) * 100;
                        allResult.Add(new Result
                        {
                            Average = average,
                            CourseId = course,
                            PercentageScore = percentage,
                            Total = sum,
                            UserId = userId,
                            Score = sum,
                            AverageInClass = classpercentage
                        });
                    }
                    db.Result.AddRange(allResult);
                    await db.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                //return false
            }
           
        }

        public int calculateExamSum(CourseExam exam)
        {
            if (exam.studentAns == (int)exam.correctAns)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int calculateExamAverage(CourseExam exam)
        {
            if (exam.studentAns == (int)exam.correctAns)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /*
         *The Dictionary key is the course Id and the value is the course title
         */
        public async Task<Dictionary<int, string>> GetRegisteredCourses(string userId)
        {
            var result = await db.CourseRegistration.Include("course1").Include("course2").Include("course3").Include("course4").Where(x => x.userId == userId).FirstOrDefaultAsync();
            if (result != null)
            {
                return new Dictionary<int, string> {
                    { result.course1Id, result.course1.name},
                    {result.course2Id, result.course2.name },
                    { result.course3Id, result.course3.name},
                    {result.course4Id, result.course4.name }
                };
            }
            return null;
        }

    }
}