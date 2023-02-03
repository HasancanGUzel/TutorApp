using OzelDers.Business.Abstract;
using OzelDers.Data.Abstract;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> FindByIdStudentAsync(string id)
        {
            return await _unitOfWork.Student.FindByIdStudentAsync(id);
        }

        public async Task<List<Student>> GetStudentsWithUser()
        {
            return await _unitOfWork.Student.GetStudentsWithUser();
        }


        public async Task CreateAsync(Student student)
        {
            await _unitOfWork.Student.CreateAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateStudentAsync(Student student, int[] selectedLessonIds)
        {
            await _unitOfWork.Student.CreateStudentAsync(student, selectedLessonIds);
        }

        public void Delete(Student student)
        {
            _unitOfWork.Student.Delete(student);
            _unitOfWork.Save();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _unitOfWork.Student.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _unitOfWork.Student.GetByIdAsync(id);
        }

        public async Task<Student> GetStudentDetailsByIdAsync(int studentid)
        {
            return await _unitOfWork.Student.GetStudentDetailsByIdAsync(studentid);
        }

        public async Task<List<Student>> GetStudentsByLessonAsync(string lesson)
        {
            return await _unitOfWork.Student.GetStudentsByLessonAsync(lesson);
        }

        public async Task<Student> GetStudentWithLessons(int id)
        {
            return await _unitOfWork.Student.GetStudentWithLessons(id);
        }

        public void Update(Student student)
        {
            _unitOfWork.Student.Update(student);
            _unitOfWork.Save();
        }
        public async Task UpdateAsyncc(Student student)
        {
            await _unitOfWork.Student.UpdateAsyncc(student);
        }

        public async Task UpdateStudentAsync(Student student, int[] selectedLessonIds)
        {
            await _unitOfWork.Student.UpdateStudentAsync(student, selectedLessonIds);
        }

        public async Task<List<Student>> GetHomePageStudentsAsync()
        {
            return await _unitOfWork.Student.GetHomePageStudentsAsync();
        }
    }
}
