using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBT_APP.Models
{
    public class CourseExam
    {
        public int Id { get; set; }
        public int courseId { get; set; }
        public int studentAns { get; set; }
        public Answers correctAns { get; set; }
        public int score { get; set; }
        public string userId { get; set; }
        public Courses course { get; set; }
        public ApplicationUser user { get; set; }
    }
}