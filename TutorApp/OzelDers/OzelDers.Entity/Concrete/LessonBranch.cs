using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzelDers.Entity.Concrete
{
    public class LessonBranch
    {
         public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}