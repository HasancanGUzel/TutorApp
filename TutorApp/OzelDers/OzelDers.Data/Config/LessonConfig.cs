using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Config
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {

            builder.HasKey(l => l.Id);//primary key
            builder.Property(l => l.Id).ValueGeneratedOnAdd();//1 er 1er artması

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.Url)
             .IsRequired()
             .HasMaxLength(250);


            builder.HasData(
              new Lesson
              {
                  Id = 1,
                  Name = "Mat-1",
                  Url = "mat-1"
              },
              new Lesson
              {
                  Id = 2,
                  Name = "Geometri",
                  Url = "geometri"
              },
              new Lesson
              {
                  Id = 3,
                  Name = "Fizik",
                  Url = "fizik"
              },

                   new Lesson
                   {
                       Id = 4,
                       Name = "Vektörler",
                       Url = "vektorler"
                   },
                 new Lesson
                 {
                     Id = 5,
                     Name = "Türkçe",
                     Url = "turkce"
                 },
                   new Lesson
                   {
                       Id = 6,
                       Name = "Kimya",
                       Url = "kimya"
                   }
              );
        }
    }
}
