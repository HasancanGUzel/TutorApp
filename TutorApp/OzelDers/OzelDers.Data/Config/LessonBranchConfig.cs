using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Config
{
    public class LessonBranchConfig : IEntityTypeConfiguration<LessonBranch>
    {
        public void Configure(EntityTypeBuilder<LessonBranch> builder)
        {
            builder.HasKey(lb => new { lb.LessonId, lb.BranchId });


            builder.HasData(
               new LessonBranch { LessonId = 1, BranchId = 2 },
               new LessonBranch { LessonId = 2, BranchId = 2 },
               new LessonBranch { LessonId = 3, BranchId = 3 },
               new LessonBranch { LessonId = 4, BranchId = 3 },
               new LessonBranch { LessonId = 5, BranchId = 1 }
               );




        }
    }
}
