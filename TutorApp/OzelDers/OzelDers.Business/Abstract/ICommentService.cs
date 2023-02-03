using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Business.Abstract
{
    public interface ICommentService
    {
        Task<Comment> GetByIdAsync(int id);
        Task<List<Comment>> GetAllAsync();
        Task CreateAsync(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);

        Task<List<Comment>> GetByTeacherId(int teacherId);

    }
}
