using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Abstract
{
    public interface IBranchRepository:IRepository<Branch>
    {
        Task<List<Branch>> GetBranchsWithLessons();
        Task CreateBranchAsync(Branch branch, int[] selectedLessonIds);
        Task UpdateBranchAsync(Branch branch, int[] selectedLessonIds);

        Task<Branch> GetBranchWithLessons(int id);



    }
}
