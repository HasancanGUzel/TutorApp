using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OzelDers.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OzelDers.Entity.Concrete;

namespace OzelDers.Data.Config
{
    public class TeacherLessonConfig : IEntityTypeConfiguration<TeacherLesson>
    {
        public void Configure(EntityTypeBuilder<TeacherLesson> builder)
        {
            builder.HasKey(tl => new { tl.TeacherId, tl.LessonId });

            builder.HasData(
              new TeacherLesson { TeacherId = 1, LessonId = 1 },
              new TeacherLesson { TeacherId = 2, LessonId = 2 },
              new TeacherLesson { TeacherId = 3, LessonId = 3 },
              new TeacherLesson { TeacherId = 4, LessonId = 4 },
              new TeacherLesson { TeacherId = 5, LessonId = 5 },
              new TeacherLesson { TeacherId = 6, LessonId = 6 }

              );




        }
    }
}
