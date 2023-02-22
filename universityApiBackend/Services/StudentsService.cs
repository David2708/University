using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Services
{
    public class StudentsService : IStudentsServices
    {
        // TODO: resolve Methods
        public IEnumerable<Student> GetStudentsWithCourses(List<Student> studentsList)
        {
            var sutudentWithCourses = from student in studentsList
                                      where student.Courses.Count() > 0
                                      select student;
            return sutudentWithCourses;
        }

        public IEnumerable<Student> GetStudentsWithNotCourses(List<Student> studentsList)
        {
            var sutudentWithNoCourses = from student in studentsList
                                        where student.Courses.Count() == 0
                                      select student;
            return sutudentWithNoCourses;
        }


    }
}
