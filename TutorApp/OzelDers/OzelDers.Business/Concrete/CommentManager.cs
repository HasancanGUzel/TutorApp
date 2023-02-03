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
    public class CommentManager : ICommentService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task CreateAsync(Comment comment)
        {
            await _unitOfWork.Comment.CreateAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(Comment comment)
        {
            _unitOfWork.Comment.Delete(comment);
            _unitOfWork.Save();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _unitOfWork.Comment.GetAllAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _unitOfWork.Comment.GetByIdAsync(id);
        }



        public async Task<List<Comment>> GetByTeacherId(int teacherId)
        {
            return await _unitOfWork.Comment.GetByTeacherId(teacherId);

        }

        public void Update(Comment comment)
        {
            _unitOfWork.Comment.Update(comment);
            _unitOfWork.Save();
        }


    }
}
