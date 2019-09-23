using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBT_APP.Models
{
    public class Question
    {
        public int id { get; set; }
        public string question { get; set; }
        public int courseId { get; set; }
        public string  optionA { get; set; }
        public string optionB { get; set; }
        public string optionC { get; set; }
        public string optionD { get; set; }
        //public string optionE { get; set; }
        public Answers correctAnswer { get; set; }
        public int weight { get; set; }
        public Courses Course { get; set; }
    }
}