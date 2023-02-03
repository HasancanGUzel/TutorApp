using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Business.Abstract
{
    public interface IBranchService
    {
        Task<Branch> GetByIdAsync(int id);
        Task<List<Branch>> GetAllAsync();
        Task CreateAsync(Branch branch);
        Task<List<Branch>> GetBranchsWithLessons();

        Task CreateBranchAsync(Branch branch, int[] selectedLessonIds);

        Task UpdateBranchAsync(Branch branch, int[] selectedLessonIds);

        Task<Branch> GetBranchWithLessons(int id);


        void Update(Branch branch);
        void Delete(Branch branch);
    }
}
