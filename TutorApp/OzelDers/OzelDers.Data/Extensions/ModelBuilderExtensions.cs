using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            #region RolBilgileri
            List<Role> roles = new List<Role>
            {
                new Role
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                 new Role
                {
                    Name="Teacher",
                    NormalizedName="TEACHER"
                },
                new Role
                {
                    Name="Student",
                    NormalizedName="STUDENT"
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);
            #endregion

            #region Branş
            List<Branch> branches = new List<Branch>
            {
                 new Branch
               {

                   Id = 1,
                   Name = "Sınıf Öğretmeni",
                   Url = "sinif-ogretmeni"
               },


            };
            modelBuilder.Entity<Branch>().HasData(branches);
            #endregion

            #region KullanıcıBilgileri
            List<User> users = new List<User>

            {
                new User
                {

                    UserName="admin",
                    NormalizedUserName="ADMIN",
                     Email="admin@gmail.com",
                    NormalizedEmail="ADMIN@GMAIL.COM",
                    EmailConfirmed=true,
                    PhoneNumber="5555555555",
                 },
                 new User
                {
                    UserName="Teacher",
                    NormalizedUserName="TEACHER",
                    Email="teacher@gmail.com",
                    NormalizedEmail="TEACHER@GMAIL.COM",
                    EmailConfirmed=true,
                    PhoneNumber="4444444444",

                },
                  new User
                {
                    UserName="Student",
                    NormalizedUserName="STUDENT",
                    Email="student@gmail.com",
                    NormalizedEmail="STUDENT@GMAIL.COM",
                    EmailConfirmed=true,
                    PhoneNumber="4444444444",
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher
                {
                    UserId=users[0].Id,
                    Id=1,
                    BranchId=branches[0].Id,
                    FirstName="Hasan",
                    LastName="Admin",
                    Gender="Erkek",
                    DateOfBirth=new DateTime(1985,10,29),
                    Address="Akasya cd. Orkide sk. Gül ap.",
                    City="İstanbul",
                    ImageUrl="1.png",
                    Price=250,
                    Location="Adalar",
                    Experience="1-3 Yıl Tecrübeli",
                    IsHome=true
                   
                },

                new Teacher
                {
                    UserId=users[1].Id,
                    Id=2,
                     BranchId=branches[0].Id,
                    FirstName="Sefa",
                    LastName="Teacher",
                    Gender="Erkek",
                    DateOfBirth=new DateTime(1985,10,29),
                    Address="Akasya cd. Orkide sk. Gül ap.",
                    City="İstanbul",
                    ImageUrl="1.png",
                    Price=250,
                    Location="Adalar",
                    Experience="1-3 Yıl Tecrübeli",
                    IsHome=true

                },
            };
            modelBuilder.Entity<Teacher>().HasData(teachers);

            List<Student> students = new List<Student>
            {
                new Student
                {
                    UserId=users[2].Id,
                    Id=1,
                    FirstName="Sema",
                    LastName="Student",
                    Gender="Erkek",
                    DateOfBirth=new DateTime(1985,10,29),
                    Address="Akasya cd. Orkide sk. Gül ap.",
                    City="İstanbul",
                    ImageUrl="1.png",
                    Location="Adalar",
                    IsHome=true


                },

               
            };
            modelBuilder.Entity<Student>().HasData(students);
            #endregion

           

            #region Parolaİşlemleri
            var passwordHasher = new PasswordHasher<User>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Qwe123.");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Qwe123.");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "Qwe123.");
            #endregion

            #region KullanıcıRolAtamaİşlemleri
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId=users[0].Id,
                    RoleId=roles.First(r=>r.Name=="Admin").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[1].Id,
                    RoleId=roles.First(r=>r.Name=="Teacher").Id
                },
                new IdentityUserRole<string>
                {
                    UserId=users[2].Id,
                    RoleId=roles.First(r=>r.Name=="Student").Id
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion
        }
    }
}


