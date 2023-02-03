using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {

        Task<List<Comment>> GetByTeacherId(int teacherId);

    }
}
