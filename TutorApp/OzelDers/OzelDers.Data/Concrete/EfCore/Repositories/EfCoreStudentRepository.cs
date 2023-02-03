using Microsoft.EntityFrameworkCore;
using OzelDers.Data.Abstract;
using OzelDers.Data.Concrete.EfCore.Context;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Concrete.EfCore.Repositories
{
    public class EfCoreStudentRepository : EfCoreGenericRepository<Student>, IStudentRepository
    {
       
        public EfCoreStudentRepository(DbContext context) : base(context)
        {
        }


        private OzelDersContext OzelDersContext
        {
            get { return _context as OzelDersContext; }
        }
        public async Task CreateStudentAsync(Student student, int[] selectedLessonIds)
        {
            await OzelDersContext.Students.AddAsync(student);
            await OzelDersContext.SaveChangesAsync();
            student.StudentLesson = selectedLessonIds
                .Select(catId => new StudentLesson
                {
                    StudentId = student.Id,
                    LessonId = catId
                }).ToList();
            await OzelDersContext.SaveChangesAsync();
        }



        public async Task<Student> FindByIdStudentAsync(string id) //---------------------
        {
            var student = await OzelDersContext
              .Students
              .Include(t => t.User)
               .FirstOrDefaultAsync(t => t.UserId == id);
            return student;
        }

        public async Task<List<Student>> GetHomePageStudentsAsync()
        {
            return await OzelDersContext
               .Students
               .Where(p => p.IsHome)
               .ToListAsync();
        }

        public async Task<Student> GetStudentDetailsByIdAsync(int studentid)
        {
            var student = await OzelDersContext
              .Students
               .Include(t => t.StudentLesson)
                .ThenInclude(tl => tl.Lesson)
                .FirstOrDefaultAsync(t => t.Id == studentid);
            return student;
        }

        public async Task<List<Student>> GetStudentsByLessonAsync(string lesson)
        {
            var students = OzelDersContext.Students.Where(s=>s.IsHome).AsQueryable();
            if (lesson != null)
            {
                students = students
                    .Include(t => t.StudentLesson)
                    .ThenInclude(tl => tl.Lesson)
                    .Where(p => p.StudentLesson.Any(pc => pc.Lesson.Url == lesson));
            }
            return await students.ToListAsync();
        }


        public async Task<List<Student>> GetStudentsWithUser() //-------------------
        {
            return await OzelDersContext
                 .Students
                 .Include(t => t.User)
                 .ToListAsync();
        }


        public async Task<Student> GetStudentWithLessons(int id)
        {
            return await OzelDersContext
                 .Students
                 .Where(p => p.Id == id)
                 .Include(p => p.StudentLesson)
                 .ThenInclude(pc => pc.Lesson)
                 .FirstOrDefaultAsync();
        }

        public async Task UpdateStudentAsync(Student student, int[] selectedLessonIds)
        {
            Student newStudent = await OzelDersContext
                .Students
                .Include(s => s.StudentLesson)
                .FirstOrDefaultAsync(t => t.Id == student.Id);
            newStudent.StudentLesson = selectedLessonIds
                .Select(lesId => new StudentLesson
                {
                    StudentId = newStudent.Id,
                    LessonId = lesId
                }).ToList();
            OzelDersContext.Update(newStudent);
            await OzelDersContext.SaveChangesAsync();
        }
    }
}
