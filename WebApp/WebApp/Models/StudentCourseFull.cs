using System.Collections.Generic;

namespace WebApp.Models
{
    public class StudentCourseFull
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Teacher { get; set; }
        public string Semester { get; set; }
        public int Hours { get; set; }
        public int Credit { get; set; }
        public string ExamMethod { get; set; }
        public int? Score { get; set; }
    }
}