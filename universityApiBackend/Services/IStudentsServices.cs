using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Services
{
    public interface IStudentsServices
    {
        IEnumerable<Student> GetStudentsWithCourses(List<Student> studentsList);
        IEnumerable<Student> GetStudentsWithNotCourses(List<Student> studentsList);
    }
}
