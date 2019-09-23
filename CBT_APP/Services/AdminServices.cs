using CBT_APP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CBT_APP.Services
{
    public class AdminServices
    {
        private ApplicationDbContext db;
        public AdminServices()
        {
            db = new ApplicationDbContext();
        }

        public async Task<IList<Courses>>  AllCourses(bool isExam = false)
        {
            if (isExam)
            {
                return await db.Course.Where(x => x.status == true).ToListAsync();
            }
            return await db.Course.ToListAsync();
        }

        public async Task<bool> AddCourse(Courses course)
        {
            try
            {
                db.Course.Add(course);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddQuestionsForCourse(Question question)
        {
            try
            {
                db.Question.Add(question);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}