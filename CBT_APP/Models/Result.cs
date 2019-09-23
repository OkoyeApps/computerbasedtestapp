using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBT_APP.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public double Score { get; set; }
        public double Average { get; set; }
        public double Total { get; set; }
        public double PercentageScore { get; set; }
        public double AverageInClass { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ApplicationUser User { get; set; }
        public Courses Course { get; set; }
        
    }
}