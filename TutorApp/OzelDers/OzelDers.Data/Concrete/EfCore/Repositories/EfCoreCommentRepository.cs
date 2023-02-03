using Microsoft.EntityFrameworkCore;
using OzelDers.Data.Abstract;
using OzelDers.Data.Concrete.EfCore.Context;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Concrete.EfCore.Repositories
{
    public class EfCoreCommentRepository : EfCoreGenericRepository<Comment>, ICommentRepository
    {
        public EfCoreCommentRepository(DbContext context) : base(context)
        {
        }

        private OzelDersContext OzelDersContext
        {
            get { return _context as OzelDersContext; }
        }

        public async Task<List<Comment>> GetByTeacherId(int teacherId)
        {
            return await OzelDersContext.Comments.Include(c => c.User).Where(c => c.TeacherId == teacherId).ToListAsync();
        }



    }
}
