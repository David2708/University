using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Services
{
    public class CoursesServices : ICoursesService
    {
        public IEnumerable<Course> GetCoursesByCategory(string categoryName)
        {
            List<Course> courses = new List<Course>();
            var coursesByCategory = from course in courses
                                    where course.Categories.Any(cat => cat.Name== categoryName)
                                    select course;

            return coursesByCategory;
        }

        public IEnumerable<Course> GetCoursesByStudent(int id)
        {
            List<Course> courses = new List<Course> ();
            var coursesByStudent = from course in courses
                                   where course.Students.Any(student => student.Id == id)
                                   select course;

            return coursesByStudent;
        }

        public IEnumerable<Course> GetCoursesWithoutTopics()
        {
            List<Course> courses = new List<Course>();
            var coursesWithoutTopic = from course in courses
                                   where course.Chapter == null
                                   select course;

            return coursesWithoutTopic;
        }

        public IEnumerable<Chapter> GetSillabusByCourse(int CuorseID)
        {
            List<Course> courses = new List<Course>();
            var SillabusByCourse = from course in courses
                                   where course.Id == CuorseID
                                   select course.Chapter;

            return SillabusByCourse;
        }
    }
}
