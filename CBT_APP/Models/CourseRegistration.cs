using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CBT_APP.Models
{
    public class CourseRegistration
    {
        public int Id { get; set; }
        public int course1Id { get; set; }
        public int course2Id { get; set; }
        public int course3Id { get; set; }
        public int course4Id { get; set; }
        public string userId { get; set; }
        [ForeignKey("course1Id")]
        public Courses course1 { get; set; }
        [ForeignKey("course2Id")]
        public Courses course2 { get; set; }
        [ForeignKey("course3Id")]
        public Courses course3 { get; set; }
        [ForeignKey("course4Id")]
        public Courses course4 { get; set; }
        //write the user later
        public ApplicationUser user { get; set; }

    }
}