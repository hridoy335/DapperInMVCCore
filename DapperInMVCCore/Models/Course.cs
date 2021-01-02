using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperInMVCCore.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseCredit { get; set; }
    }
}
