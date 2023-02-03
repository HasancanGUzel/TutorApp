using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IBranchRepository Branch { get; }
        ILessonRepository Lesson { get; }
        IStudentRepository Student { get; }
        ITeacherRepository Teacher { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();
        void Save();
    }
}
