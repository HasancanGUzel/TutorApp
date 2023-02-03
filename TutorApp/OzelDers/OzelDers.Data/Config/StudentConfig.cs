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
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(b => b.Id);//primary key
            builder.Property(b => b.Id).ValueGeneratedOnAdd();//1 er 1er artması

            builder.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Gender)
                   .IsRequired()
                   .HasMaxLength(10);
            builder.Property(u => u.DateOfBirth)
                 .IsRequired()
                 .HasMaxLength(10);
            builder.Property(u => u.Address)
                 .IsRequired()
                 .HasMaxLength(50);
            builder.Property(u => u.City)
                 .IsRequired()
                 .HasMaxLength(10);

            builder.Property(u => u.ImageUrl)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Location)
                .IsRequired()
                .HasMaxLength(50);



            builder.HasData(
              
               new Student
               {
                   Id = 2,
                   FirstName = "Ahmet",
                   LastName = "Üzer",
                   Gender = "Erkek",
                   DateOfBirth = new DateTime(1985, 10, 29),
                   Address = "Akasya cd. Orkide sk. Gül ap.",
                   City = "İstanbul",
                   ImageUrl = "1.png",
                   Location = "Adalar",


               },
               new Student
               {
                   Id = 3,
                   FirstName = "Sema",
                   LastName = "Kocaoğlu",
                   Gender = "Kız",
                   DateOfBirth = new DateTime(1985, 10, 29),
                   Address = "Akasya cd. Orkide sk. Gül ap.",
                   City = "İstanbul",
                   ImageUrl = "1.png",
                   Location = "Adalar",

               }
               );
        }
    }
}
