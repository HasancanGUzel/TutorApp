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
    public class EfCoreBranchRepository : EfCoreGenericRepository<Branch>, IBranchRepository
    {
        public EfCoreBranchRepository(DbContext context) : base(context)
        {
        }

        private OzelDersContext OzelDersContext
        {
            get { return _context as OzelDersContext; }
        }

        public async Task CreateBranchAsync(Branch branch, int[] selectedLessonIds)
        {
            await OzelDersContext.Branches.AddAsync(branch);  
            await OzelDersContext.SaveChangesAsync();
            branch.LessonBranch = selectedLessonIds 
                .Select(lesId => new LessonBranch
                {
                    BranchId = branch.Id,
                    LessonId = lesId,
                }).ToList();
            await OzelDersContext.SaveChangesAsync();
        }

        public async Task<List<Branch>> GetBranchsWithLessons()
        {
            return await OzelDersContext
               .Branches
               .Include(p => p.LessonBranch)
               .ThenInclude(pc => pc.Lesson)
               .ToListAsync();
        }

        public async Task<Branch> GetBranchWithLessons(int id)
        {
            return await OzelDersContext
                .Branches
                .Where(b => b.Id == id)
                .Include(b => b.LessonBranch)
                .ThenInclude(bl => bl.Lesson)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateBranchAsync(Branch branch, int[] selectedLessonIds)
        {
            Branch newBranch = await OzelDersContext 
               .Branches 
               .Include(p => p.LessonBranch)
               .FirstOrDefaultAsync(p => p.Id == branch.Id); 

            newBranch.LessonBranch = selectedLessonIds   
                .Select(catId => new LessonBranch 
                {
                    BranchId = newBranch.Id, 
                    LessonId = catId 
                }).ToList();
            OzelDersContext.Update(newBranch); 
            await OzelDersContext.SaveChangesAsync();
        }
    }
}
