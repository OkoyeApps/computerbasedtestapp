using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBT_APP.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public bool status { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual List<Question> Questions { get; set; }
    }
}