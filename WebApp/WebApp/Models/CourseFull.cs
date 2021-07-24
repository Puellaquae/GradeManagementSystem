using System.Collections.Generic;

namespace WebApp.Models
{
    public class CourseFull
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> TeacherId { get; set; }
        public List<string> TeacherName { get; set; }
        public string Semester { get; set; }
        public int Hours { get; set; }
        public int Credit { get; set; }
        public string ExamMethod { get; set; }
    }
}