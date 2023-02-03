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
    public class StudentLessonConfig : IEntityTypeConfiguration<StudentLesson>
    {

        public void Configure(EntityTypeBuilder<StudentLesson> builder)
        {
            builder.HasKey(tl => new { tl.StudentId, tl.LessonId });

            builder.HasData(
              new StudentLesson { StudentId = 1, LessonId = 1 },
              new StudentLesson { StudentId = 2, LessonId = 2 },
              new StudentLesson { StudentId = 3, LessonId = 3 }
            

              );
        }
    }
}
