using OzelDers.Business.Abstract;
using OzelDers.Data.Abstract;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Business.Concrete
{
    public class TeacherManager : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Teacher> FindByIdTeacherAsync(string id)
        {
            return await _unitOfWork.Teacher.FindByIdTeacherAsync(id);
        }

        public async Task CreateAsync(Teacher teacher)
        {
            await _unitOfWork.Teacher.CreateAsync(teacher);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateTeacherAsync(Teacher teacher, int[] selectedLessonIds)
        {
            await _unitOfWork.Teacher.CreateTeacherAsync(teacher, selectedLessonIds);
        }

        public void Delete(Teacher teacher)
        {
            _unitOfWork.Teacher.Delete(teacher);
            _unitOfWork.Save();
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _unitOfWork.Teacher.GetAllAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _unitOfWork.Teacher.GetByIdAsync(id);
        }

        public async Task<Teacher> GetTeacherDetailsByIdAsync(int teacherid)
        {
            return await _unitOfWork.Teacher.GetTeacherDetailsByIdAsync(teacherid);
        }

        public async Task<List<Teacher>> GetTeachersByLessonAsync(string lesson)
        {
            return await _unitOfWork.Teacher.GetTeachersByLessonAsync(lesson);
        }

        public async Task<List<Teacher>> GetTeachersWithUser()
        {
            return await _unitOfWork.Teacher.GetTeachersWithUser();
        }

        public async Task<Teacher> GetTeacherWithLessons(int id)
        {
            return await _unitOfWork.Teacher.GetTeacherWithLessons(id);
        }

        public void Update(Teacher teacher)
        {
            _unitOfWork.Teacher.Update(teacher);
            _unitOfWork.Save();
        }

        public async Task UpdateAsyncc(Teacher teacher)
        {
             await _unitOfWork.Teacher.UpdateAsyncc(teacher);
        }

        public async Task UpdateTeacherAsync(Teacher teacher, int[] selectedLessonIds)
        {
            await _unitOfWork.Teacher.UpdateTeacherAsync(teacher, selectedLessonIds);
        }

        public async Task<List<Teacher>> GetSearchResultsAsync(string searchString)
        {
            return await _unitOfWork.Teacher.GetSearchResultsAsync(searchString);
        }

        public async Task<List<Teacher>> GetHomePageTeachersAsync()
        {
            return await _unitOfWork.Teacher.GetHomePageTeachersAsync();
        }
    }
}
