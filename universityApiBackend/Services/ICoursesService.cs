using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetCoursesByCategory(string categoryName);
        IEnumerable<Course> GetCoursesWithoutTopics();
        IEnumerable<Chapter> GetSillabusByCourse(int CuorseID);
        IEnumerable<Course> GetCoursesByStudent(int id);
    }
}
