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
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {


        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(u => u.Id);//primary key
            builder.Property(u => u.Id).ValueGeneratedOnAdd();//1 er 1er artması

            builder.Property(u => u.FirstName)
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
                   .HasMaxLength(20);

            builder.Property(u => u.Address)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(u => u.City)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(50);

           

            builder.Property(u => u.Price)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(u => u.Location)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(u => u.Experience)
                  .IsRequired()
                  .HasMaxLength(15);

            builder.HasOne(b => b.Branch)
               .WithMany(u => u.Teachers)
               .HasForeignKey(u => u.BranchId);


            builder.HasData(
             
               new Teacher
               {
                   Id = 3,
                   BranchId = 1,
                   FirstName = "Ayşe",
                   LastName = "Gündoğdu",
                   DateOfBirth = new DateTime(1985, 10, 29),
                   Gender = "Kız",
                   Address = "Akasya cd. Orkide sk. Gül ap.",
                   City = "İstanbul",
                   ImageUrl = "1.png",
                   Price = 250,
                   Location = "Adalar",
                   Experience = "1-3 Yıl Tecrübeli"
                   

               },
                new Teacher
                {
                    Id = 4,
                    BranchId = 3,

                    FirstName = "Fatma",
                    LastName = "Üçtepe",
                    DateOfBirth = new DateTime(1985, 10, 29),
                    Gender = "Kız",
                    Address = "Akasya cd. Orkide sk. Gül ap.",
                    City = "İstanbul",
                    ImageUrl = "1.png",
                    Price = 250,
                    Location = "Adalar",
                    Experience = "1-3 Yıl Tecrübeli"

                },
                 new Teacher
                 {
                     Id = 5,
                     BranchId = 3,

                     FirstName = "Esra",
                     LastName = "Yüksel",
                     Gender = "Kız",
                     DateOfBirth = new DateTime(1985, 10, 29),
                     
                     Address = "Akasya cd. Orkide sk. Gül ap.",
                     City = "İstanbul",
                     ImageUrl = "1.png",
                     Price = 250,
                     Location = "Adalar",
                     Experience = "1-3 Yıl Tecrübeli"

                 },
                  new Teacher
                  {
                      Id = 6,
                      BranchId = 2,
                      Gender = "Erkek",
                      FirstName = "Taha",
                      LastName = "Yılmaz",
                      DateOfBirth = new DateTime(1985, 10, 29),
                      Address = "Akasya cd. Orkide sk. Gül ap.",
                      City = "İstanbul",
                      ImageUrl = "1.png",
                      Price = 250,
                      Location = "Adalar",
                      Experience = "1-3 Yıl Tecrübeli"

                  }
                  );
        }
    }
    }
