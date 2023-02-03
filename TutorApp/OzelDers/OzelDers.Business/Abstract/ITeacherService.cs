using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Business.Abstract
{
    public interface ITeacherService
    {
        Task<Teacher> GetByIdAsync(int id);
        Task<List<Teacher>> GetAllAsync();
        Task CreateAsync(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
        Task UpdateAsyncc(Teacher teacher);
        Task<List<Teacher>> GetTeachersByLessonAsync(string lesson);
        Task<List<Teacher>> GetTeachersWithUser();//anasayfada öğretmenleri listelerken branşlarıda getiricez

        Task<Teacher> GetTeacherDetailsByIdAsync(int teacherid);

        Task CreateTeacherAsync(Teacher teacher, int[] selectedLessonIds);

        Task<Teacher> GetTeacherWithLessons(int id);

        Task UpdateTeacherAsync(Teacher teacher, int[] selectedLessonIds);

        Task<Teacher> FindByIdTeacherAsync(string id);

        Task<List<Teacher>> GetSearchResultsAsync(string searchString);

        Task<List<Teacher>> GetHomePageTeachersAsync();






    }
}
