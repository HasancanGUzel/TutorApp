using Microsoft.EntityFrameworkCore;
using OzelDers.Data.Abstract;
using OzelDers.Data.Concrete.EfCore.Context;
using OzelDers.Data.Concrete.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OzelDersContext _context;
        public UnitOfWork(OzelDersContext context)
        {
            _context = context;
        }


        private EfCoreBranchRepository _branchRepository;
        private EfCoreLessonRepository _lessonRepository;
        private EfCoreTeacherRepository _teacherRepository;
        private EfCoreStudentRepository _studentRepository;
        private EfCoreCommentRepository _commentRepository;
        public IBranchRepository Branch => _branchRepository = _branchRepository ?? new EfCoreBranchRepository(_context);

        public ILessonRepository Lesson => _lessonRepository = _lessonRepository ?? new EfCoreLessonRepository(_context);

        public IStudentRepository Student => _studentRepository = _studentRepository ?? new EfCoreStudentRepository(_context);

        public ITeacherRepository Teacher => _teacherRepository = _teacherRepository ?? new EfCoreTeacherRepository(_context);
        public ICommentRepository Comment => _commentRepository = _commentRepository ?? new EfCoreCommentRepository(_context);

        public void Dispose()
        {
            _context.Dispose();

        }

        public void Save()
        {
            _context.SaveChanges();

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
