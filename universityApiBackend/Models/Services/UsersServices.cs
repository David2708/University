using System.Collections.Generic;
using System.Linq;
using universityApiBackend.Models.DataModels;
using static universityApiBackend.Models.DataModels.Course;

namespace universityApiBackend.Models.Services
{
    public class UsersServices
    {
        public IEnumerable<User>? SearchUsersByEmail(List<User> listUSers, string email)
        {
            var usersByEMail = from user in listUSers where user.Email == email
                               select user;

            return usersByEMail;
        }

        public IEnumerable<Student>? LookForOlderStudent(List<Student> listStudents)
        {
            var currentyear = DateTime.Now;
            var studentsOfLegalAge = from student in listStudents
                               where student.DOB.Year - currentyear.Year >= 18
                               select student;

            return studentsOfLegalAge;
        }

        public IEnumerable<Student>? SearchStudentsWhoHaveAtLeastOneCourse(List<Student> listStudents)
        {
            var studentThanHaveMoreACourse = from student in listStudents
                                             where student.Courses.Count() >= 1
                                             select student;

            return studentThanHaveMoreACourse;
        }

        public IEnumerable<Course>? SearchCourseByLevelThanHaveOneOrMoreStudent(List<Course> listCourses, Levels level)
        {
            var CousesByLevelWithMoreStudent = from course in listCourses
                                             where course.Level == level && course.Students.Count() >= 1
                                             select course;

            return CousesByLevelWithMoreStudent;
        }

        public IEnumerable<Course>? SearchCourseByLevelAndCategory(List<Course> listCourses, Levels level, Category category)
        {
            var CousesByLevelAndCategory = from course in listCourses
                                            where course.Level == level && course.Categories == category
                                            select course;

            return CousesByLevelAndCategory;
        }


    }
}
