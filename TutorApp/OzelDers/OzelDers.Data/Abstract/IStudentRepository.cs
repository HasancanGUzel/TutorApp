using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Abstract
{
    public interface IStudentRepository : IRepository<Student>
    {

        Task CreateStudentAsync(Student student, int[] selectedLessonIds);

        Task<Student> GetStudentWithLessons(int id);

        Task UpdateStudentAsync(Student student, int[] selectedLessonIds);

        Task<Student> GetStudentDetailsByIdAsync(int studentid);
        Task<List<Student>> GetStudentsByLessonAsync(string lesson);

        Task<List<Student>> GetStudentsWithUser();

        Task<Student> FindByIdStudentAsync(string id);
        Task<List<Student>> GetHomePageStudentsAsync();
    }
}
