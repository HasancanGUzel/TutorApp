using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Business.Abstract
{
    public interface ILessonService
    {
        Task<Lesson> GetByIdAsync(int id);
        Task<List<Lesson>> GetAllAsync();
        Task CreateAsync(Lesson lesson);
        void Update(Lesson lesson);
        void Delete(Lesson lesson);
    }
}
