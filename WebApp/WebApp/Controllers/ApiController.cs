using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [EnableCors("all")]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly GradeDbContext db = new();
        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("major:all")]
        public List<Major> GetAllMajors()
        {
            var res = from m in db.Major select m;
            return res.ToList();
        }

        [HttpGet("major:{majorId}/class:all")]
        public List<Class> GetAllClasses(string majorId)
        {
            var res = from c in db.Class where c.MajorId == majorId select c;
            return res.ToList();
        }

        [HttpPut("class")]
        public void UpdateClass(Class[] newClasses)
        {
            foreach (var @class in newClasses)
            {
                var res = from c in db.Class where c.Id == @class.Id select c;
                if(!res.Any()) continue;
                res.Single().Name = @class.Name;
            }

            db.SaveChanges();
        }

        [HttpPost("class:{classId}")]
        public void AddClass(string classId, string name, string majorId)
        {
            db.Class.Add(new Class{Id = classId, MajorId = majorId,Name = name});
            db.SaveChanges();
        }

        [HttpDelete("class:{classId}")]
        public void DeleteClass(string classId)
        {
            var dc = (from c in db.Class where c.Id == classId select c);
            if (!dc.Any())
            {
                return;
            }
            db.Class.Remove(dc.First());
            db.SaveChanges();
        }

        [HttpGet("class:{classId}/course:all")]
        public List<CourseFull> GetClassAllCourses(string classId)
        {
            var res = from cc in db.ClassCourse
                      where cc.ClassId == classId
                      select new CourseFull
                      {
                          Id = cc.CourseId,
                          Name = cc.CourseNavigation.Name,
                          Credit = cc.CourseNavigation.Credit,
                          ExamMethod = cc.CourseNavigation.ExamMethod,
                          Hours = cc.CourseNavigation.Hours,
                          Semester = cc.CourseNavigation.Semester,
                      };
            var list = res.ToList();
            foreach (CourseFull courseFull in list)
            {
                courseFull.TeacherId = (from t in db.TeacherCourse
                        where t.CourseId == courseFull.Id
                        select t.TeacherNavigation.UserNavigation.Id
                    ).ToList();

                courseFull.TeacherName = (from t in db.TeacherCourse
                        where t.CourseId == courseFull.Id
                        select t.TeacherNavigation.UserNavigation.Name
                    ).ToList();
            }

            return list;
        }

        [HttpGet("class:{classId}/student:all")]
        public List<StudentFull> GetClassAllStudents(string classId)
        {
            var res = from sf in db.StudentFull where sf.ClassId == classId select sf;
            return res.ToList();
        }

        [HttpPost("class:{classId}/course:{courseId}")]
        public void AddCourseToClass(string classId, string courseId)
        {
            db.ClassCourse.Add(new ClassCourse
            {
                CourseId = courseId,
                ClassId = classId
            });

            var stus = (from s in db.StudentFull where s.ClassId == classId select s).ToList();

            foreach (var stu in stus)
            {
                db.StudentCourse.Add(new StudentCourse
                {
                    CourseId = courseId,
                    StudentId = stu.StudentId
                });
            }

            db.SaveChanges();
        }

        [HttpDelete("class:{classId}/course:{courseId}")]
        public void DeleteCourseFromClass(string classId, string courseId)
        {
            db.ClassCourse.Remove(new ClassCourse
            {
                CourseId = courseId,
                ClassId = classId
            });

            var stus = (from s in db.StudentFull where s.ClassId == classId select s).ToList();

            foreach (var stu in stus)
            {
                db.StudentCourse.Remove(new StudentCourse
                {
                    CourseId = courseId,
                    StudentId = stu.StudentId
                });
            }

            db.SaveChanges();
        }

        [HttpPost("student:{studentId}")]
        public void AddStudent(string studentId, string name, int age, string gender, string classId, string sourcePlace)
        {
            db.User.Add(new User
            {
                Age = age,
                Gender = gender,
                Id = studentId,
                Name = name,
                Password = "123456",
                Role = "STUDENT"
            });
            db.Student.Add(new Student
            {
                ClassId = classId,
                SourcePlace = sourcePlace,
                UserId = studentId
            });
            db.SaveChanges();
        }

        [HttpPut("student")]
        public void UpdateStudent(StudentFull[] students)
        {
            foreach (var student in students)
            {
                var res = from s in db.Student where s.UserId == student.StudentId select s;
                if(!res.Any()) continue;

                var dbs = res.Single();
                dbs.SourcePlace = student.SourcePlace;

                var resu = from u in db.User where u.Id == student.StudentId select u;
                if (!resu.Any()) continue;

                var dbu = resu.Single();
                dbu.Age = student.Age;
                dbu.Name = student.Name;
                dbu.Gender = student.Gender;
            }

            db.SaveChanges();
        }

        [HttpGet("student:{studentId}/course:all")]
        public List<StudentCourseFull> GetStudentAllCourses(string studentId)
        {
            var res = from sc in db.StudentCourse
                      where sc.StudentId == studentId
                      select new StudentCourseFull
                      {
                          Credit = sc.CourseNavigation.Credit,
                          ExamMethod = sc.CourseNavigation.ExamMethod,
                          Hours = sc.CourseNavigation.Hours,
                          Id = sc.CourseId,
                          Name = sc.CourseNavigation.Name,
                          Score = sc.Score,
                          Semester = sc.CourseNavigation.Semester
                      };
            var list = res.ToList();
            foreach (var c in list)
            {
                c.Teacher = (from t in db.TeacherCourse
                             where t.CourseId == c.Id
                             select t.TeacherNavigation.UserNavigation.Name).ToList();
            }

            return list;
        }

        [HttpGet("teacher:all")]
        public List<TeacherFull> GetAllTeachers()
        {
            var res = from t in db.Teacher
                      select new TeacherFull
                      {
                          Age = t.UserNavigation.Age,
                          Gender = t.UserNavigation.Gender,
                          Name = t.UserNavigation.Name,
                          TeacherId = t.UserId,
                          Telephone = t.Telephone,
                          Title = t.Title
                      };

            return res.ToList();
        }

        [HttpGet("teacher:{teacherId}/course:all")]
        public List<CourseFull> GetTeacherAllCourses(string teacherId)
        {
            var res = from tc in db.TeacherCourse
                      where tc.TeacherId == teacherId
                      select new CourseFull
                      {
                          Id = tc.CourseId,
                          Name = tc.CourseNavigation.Name,
                          Credit = tc.CourseNavigation.Credit,
                          ExamMethod = tc.CourseNavigation.ExamMethod,
                          Hours = tc.CourseNavigation.Hours,
                          Semester = tc.CourseNavigation.Semester,
                      };
            var list = res.ToList();
            foreach (CourseFull courseFull in list)
            {
                courseFull.TeacherId = (from t in db.TeacherCourse
                        where t.CourseId == courseFull.Id
                        select t.TeacherNavigation.UserNavigation.Id
                    ).ToList();

                courseFull.TeacherName = (from t in db.TeacherCourse
                        where t.CourseId == courseFull.Id
                        select t.TeacherNavigation.UserNavigation.Name
                    ).ToList();
            }

            return list;
        }

        [HttpPost("teacher:{teacherId}")]
        public void AddTeacher(string teacherId, string name, int age, string gender, string title, string telephone)
        {
            db.User.Add(new User
            {
                Age = age,
                Gender = gender,
                Id = teacherId,
                Name = name,
                Password = "123456",
                Role = "TEACHER"
            });
            db.Teacher.Add(new Teacher
            {
                Telephone = telephone,
                Title = title,
                UserId = teacherId
            });
            db.SaveChanges();
        }

        [HttpPost("teacher")]
        public void AddTeacher(TeacherFull teacher)
        {
            db.User.Add(new User
            {
                Age = teacher.Age,
                Gender = teacher.Gender,
                Id = teacher.TeacherId,
                Name = teacher.Name,
                Password = "123456",
                Role = "TEACHER"
            });
            db.Teacher.Add(new Teacher
            {
                Telephone = teacher.Telephone,
                Title = teacher.Title,
                UserId = teacher.TeacherId
            });
            db.SaveChanges();
            
        }

        [HttpPut("teacher")]
        public void UpdateTeacher(TeacherFull[] teachers)
        {
            foreach (var teacher in teachers)
            {
                var res = from t in db.Teacher where t.UserId == teacher.TeacherId select t;
                if(!res.Any()) continue;

                var dbt = res.Single();
                dbt.Title = teacher.Title;
                dbt.Telephone = teacher.Telephone;

                var resu = from u in db.User where u.Id == teacher.TeacherId select u;
                if(!resu.Any()) continue;

                var dbu = resu.Single();

                dbu.Name = teacher.Name;
                dbu.Age = teacher.Age;
                dbu.Gender = teacher.Gender;
            }

            db.SaveChanges();
        }

        [HttpDelete("teacher:{teacherId}")]
        public void DeleteTeacher(string teacherId)
        {
            var res = from u in db.User where u.Id == teacherId select u;
            if(!res.Any()) return;

            var dbt = res.Single();

            db.User.Remove(dbt);

            db.SaveChanges();
        }

        [HttpDelete("student:{studentId}")]
        public void DeleteStudent(string studentId)
        {
            var res = from u in db.User where u.Id == studentId select u;
            if (!res.Any()) return;

            var dbt = res.Single();

            db.User.Remove(dbt);

            db.SaveChanges();
        }

        [HttpDelete("user:{id}")]
        public void DeleteUser(string id)
        {
            var res = from u in db.User where u.Id == id select u;
            if (!res.Any()) return;

            var dbt = res.Single();

            db.User.Remove(dbt);

            db.SaveChanges();
        }

        [HttpGet("course:all")]
        public List<CourseFull> GetAllCourses()
        {
            var res = from cc in db.Course
                select new CourseFull
                {
                    Id = cc.Id,
                    Name = cc.Name,
                    Credit = cc.Credit,
                    ExamMethod = cc.ExamMethod,
                    Hours = cc.Hours,
                    Semester = cc.Semester,
                };

            var list = res.ToList();
            foreach (var courseFull in list)
            {
                courseFull.TeacherId = (from t in db.TeacherCourse
                        where t.CourseId == courseFull.Id
                        select t.TeacherNavigation.UserNavigation.Id
                    ).ToList();

                courseFull.TeacherName = (from t in db.TeacherCourse
                        where t.CourseId == courseFull.Id
                        select t.TeacherNavigation.UserNavigation.Name
                    ).ToList();
            }

            return list;
        }

        [HttpPost("course")]
        public void AddCourse(CourseFull course)
        {
            db.Course.Add(new Course
            {
                Credit = course.Credit,
                ExamMethod = course.ExamMethod,
                Hours = course.Hours,
                Id = course.Id,
                Name = course.Name,
                Semester = course.Semester
            });

            foreach (var teacher in course.TeacherId)
            {
                db.TeacherCourse.Add(new TeacherCourse
                {
                    TeacherId = teacher,
                    CourseId = course.Id
                });
            }

            db.SaveChanges();
        }

        [HttpPut("course")]
        public void UpdateCourse(CourseFull[] courses)
        {
            foreach (var course in courses)
            {
                var res = from c in db.Course where c.Id == course.Id select c;
                if(!res.Any()) continue;

                var dbc = res.Single();

                dbc.Credit = course.Credit;
                dbc.ExamMethod = course.ExamMethod;
                dbc.Hours = course.Hours;
                dbc.Name = course.Name;
                dbc.Semester = course.Semester;
                
                db.TeacherCourse.RemoveRange(from tc in db.TeacherCourse where tc.CourseId == course.Id select tc);
                
                foreach (var tid in course.TeacherId)
                {
                    db.TeacherCourse.Add(new TeacherCourse
                    {
                        CourseId = course.Id,
                        TeacherId = tid
                    });
                }
            }

            db.SaveChanges();
        }

        [HttpDelete("course:{courseId}")]
        public void DeleteCourse(string courseId)
        {
            var res = from c in db.Course where c.Id == courseId select c;
            if(!res.Any()) return;

            db.Course.Remove(res.Single());
            db.SaveChanges();
        }

        [HttpGet("course:{courseId}/student:all")]
        public List<StudentCourseScore> GetCourseAllStudents(string courseId)
        {
            var res = from sc in db.StudentCourse
                where sc.CourseId == courseId
                select new StudentCourseScore
                {
                    Id = sc.StudentNavigation.UserId,
                    Name = sc.StudentNavigation.UserNavigation.Name,
                    Score = sc.Score,
                };

            return res.ToList();
        }

        [HttpPut("course:{courseId}/student:all")]
        public void UpdateCourseAllStudents(string courseId, StudentCourseScore[] scores)
        {
            foreach (var score in scores)
            {
                var res = from sc in db.StudentCourse where sc.CourseId == courseId && sc.StudentId == score.Id select sc;
                if (!res.Any()) continue;
                res.First().Score = score.Score;
            }

            db.SaveChanges();
        }

        [HttpPut("course:{courseId}/student:{studentId}")]
        [HttpPut("student:{studentId}/course:{courseId}")]
        public void UpdateCourseAllStudents(string courseId, string studentId, int score)
        {
            var res = from sc in db.StudentCourse where sc.CourseId == courseId && sc.StudentId == studentId select sc;
            if (!res.Any()) return;
            res.First().Score = score;

            db.SaveChanges();
        }

        [HttpGet("course:all:avg")]
        public List<CourseAverageScore> GetAllCourseScores()
        {
            var res = from sc in db.StudentCourse
                group sc by sc.CourseId
                into cs
                select new CourseAverageScore
                {
                    Id = cs.Key,
                    AverageScore = cs.Average(c => c.Score)
                };

            var list = res.ToList();

            foreach (var c in list)
            {
                c.Teacher = (from t in db.TeacherCourse
                    where t.CourseId == c.Id
                    select t.TeacherNavigation.UserNavigation.Name).ToList();

                var resc = (from cc in db.Course where cc.Id == c.Id select cc).First();

                c.Name = resc.Name;
                c.Credit = resc.Credit;
                c.Semester = resc.Semester;
                c.ExamMethod = resc.ExamMethod;
                c.Hours = resc.Hours;
            }

            return list;
        }

        [HttpGet("student:{studentId}/credit")]
        public int GetStudentCredit(string studentId)
        {
            var res = from scc in (from sc in db.StudentCourse
                where sc.StudentId == studentId && sc.Score >= 60
                join c in db.Course on sc.CourseId equals c.Id
                select new {sc.StudentId, sc.CourseId, c.Credit})
                group scc by scc.StudentId
                into scs
                select scs.Sum(s => s.Credit);

            if (!res.Any())
            {
                return 0;
            }

            var credit = res.Single();

            var res2 = (from s in db.Student where s.UserId == studentId select s).Single();
            if (res2.Credit != credit)
            {
                res2.Credit = credit;
                db.SaveChanges();
            }

            return credit;
        }

        [HttpGet("student:all/grade:rank")]
        public List<StudentGradeFull> GetStudentGradesSortByGrade()
        {
            var res = from scsc in (from sc in db.StudentCourse
                    join s in db.Student on sc.StudentId equals s.UserId
                    join c in db.Course on sc.CourseId equals c.Id
                    select new {sc, s, c})
                group scsc by scsc.s.UserId
                into sscs
                select new StudentGradeFull
                {
                    StudentId = sscs.Key,
                    Grade = (double) sscs.Average(s => s.sc.Score)
                };

            var list = res.OrderByDescending(s => s.Grade).ToList();
            foreach (var stu in list)
            {
                var sscs = (from s in db.StudentFull where s.StudentId == stu.StudentId select s).Single();
                stu.Name = sscs.Name;
                stu.Age = sscs.Age;
                stu.Gender = sscs.Gender;
                stu.ClassId = sscs.ClassId;
                stu.Class = sscs.Class;
                stu.Major = sscs.Major;
                stu.MajorId = sscs.MajorId;
                stu.SourcePlace = sscs.SourcePlace;
            }

            return list;
        }

        [HttpGet("student:all/source-place:count")]
        public List<SourcePlaceCount> GetSourcePlaceCounts()
        {
            var res = from s in db.StudentFull
                group s by s.SourcePlace
                into ss
                select new SourcePlaceCount
                {
                    SourcePlace = ss.Key,
                    Count = ss.Count()
                };

            return res.ToList();
        }
    }
}