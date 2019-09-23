using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CBT_APP.Models
{
    public class CourseViewModel
    {
        [Required(ErrorMessage ="Course name must be provided")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Course Code must be provided")]
        public string Code { get; set; }
    }
}