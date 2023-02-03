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
    public class BranchManager:IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task CreateAsync(Branch branch)
        {
            await _unitOfWork.Branch.CreateAsync(branch);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateBranchAsync(Branch branch, int[] selectedLessonIds)
        {
            await _unitOfWork.Branch.CreateBranchAsync(branch, selectedLessonIds);
        }

        public void Delete(Branch branch)
        {
            _unitOfWork.Branch.Delete(branch);
            _unitOfWork.Save();
        }

        public async Task<List<Branch>> GetAllAsync()
        {
            return await _unitOfWork.Branch.GetAllAsync();
        }

        public async Task<List<Branch>> GetBranchsWithLessons()
        {
            return await _unitOfWork.Branch.GetBranchsWithLessons();

        }

        public async Task<Branch> GetBranchWithLessons(int id)
        {
            return await _unitOfWork.Branch.GetBranchWithLessons(id);
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _unitOfWork.Branch.GetByIdAsync(id);
        }

        public void Update(Branch branch)
        {
            _unitOfWork.Branch.Update(branch);
            _unitOfWork.Save();
        }

        public async Task UpdateBranchAsync(Branch branch, int[] selectedLessonIds)
        {
            await _unitOfWork.Branch.UpdateBranchAsync(branch, selectedLessonIds);
        }
    }
}
