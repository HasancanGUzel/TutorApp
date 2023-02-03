using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Abstract
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<List<Teacher>> GetTeachersByLessonAsync(string lesson);
        Task<List<Teacher>> GetTeachersWithUser();//ansayfada öğretmenleri listelerken branşlarıda getirmek için
        Task<Teacher> GetTeacherDetailsByIdAsync(int teacherid);

        Task CreateTeacherAsync(Teacher teacher, int[] selectedLessonIds);

        Task<Teacher> GetTeacherWithLessons(int id);

        Task UpdateTeacherAsync(Teacher teacher, int[] selectedLessonIds);

        Task<Teacher> FindByIdTeacherAsync(string id);

        Task<List<Teacher>> GetSearchResultsAsync(string searchString);

        Task<List<Teacher>> GetHomePageTeachersAsync();




    }
}
