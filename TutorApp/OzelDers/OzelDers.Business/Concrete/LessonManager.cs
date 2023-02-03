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
    public class LessonManager : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task CreateAsync(Lesson lesson)
        {
            await _unitOfWork.Lesson.CreateAsync(lesson);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(Lesson lesson)
        {
            _unitOfWork.Lesson.Delete(lesson);
            _unitOfWork.Save();
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _unitOfWork.Lesson.GetAllAsync();
        }

        public async Task<Lesson> GetByIdAsync(int id)
        {
            return await _unitOfWork.Lesson.GetByIdAsync(id);
        }

        public void Update(Lesson lesson)
        {
            _unitOfWork.Lesson.Update(lesson);
            _unitOfWork.Save();
        }
    }
}
