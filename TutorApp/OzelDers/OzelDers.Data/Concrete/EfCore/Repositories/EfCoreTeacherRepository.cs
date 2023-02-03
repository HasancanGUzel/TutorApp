using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OzelDers.Data.Abstract;
using OzelDers.Data.Concrete.EfCore.Context;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Concrete.EfCore.Repositories
{
    public class EfCoreTeacherRepository : EfCoreGenericRepository<Teacher>, ITeacherRepository
    {
        public EfCoreTeacherRepository(DbContext context) : base(context)
        {
        }

        private OzelDersContext OzelDersContext
        {
            get { return _context as OzelDersContext; }
        }

        public async Task CreateTeacherAsync(Teacher teacher, int[] selectedLessonIds)
        {
            await OzelDersContext.Teachers.AddAsync(teacher);
            await OzelDersContext.SaveChangesAsync();
            teacher.TeacherLesson = selectedLessonIds
                .Select(catId => new TeacherLesson
                {
                    TeacherId = teacher.Id,
                    LessonId = catId
                }).ToList();
            await OzelDersContext.SaveChangesAsync();
        }



        public async Task<Teacher> FindByIdTeacherAsync(string id) //------------------------
        {
            var teacher = await OzelDersContext
             .Teachers
             .Include(t => t.User)
              .FirstOrDefaultAsync(t => t.UserId == id);
            return teacher;
        }

        public async Task<List<Teacher>> GetHomePageTeachersAsync()
        {
            return await OzelDersContext
                .Teachers
                .Where(p => p.IsHome)
                .ToListAsync();
        }

        public   async Task<List<Teacher>> GetSearchResultsAsync(string searchString)
        {
            searchString=searchString.ToLower();
            searchString = searchString.Trim();
            var teachers = OzelDersContext.Teachers.Where(t=>t.IsHome).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                return await  teachers.Where(t => t.FirstName.ToLower().Trim().Contains(searchString)).ToListAsync();

            }
            return await teachers.ToListAsync();
            
           



        }

        public async Task<Teacher> GetTeacherDetailsByIdAsync(int teacherid)
        {
            //return OzelDersContext
            //   .Teachers
            //   .Where(p => p.Id == teacherid)
            //   .Include(p => p.Branch)
            //   .FirstOrDefaultAsync();



            var teacher = await OzelDersContext
              .Teachers
               .Include(t => t.TeacherLesson)
                .ThenInclude(tl => tl.Lesson)
                .Include(t => t.Branch)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == teacherid);
            return teacher;


        }

        public async Task<List<Teacher>> GetTeachersByLessonAsync(string lesson)
        {
            var teachers = OzelDersContext.Teachers.Where(t=>t.IsHome).AsQueryable(); //-- burası
            if (lesson != null)
            {
                teachers = teachers
                    .Include(t => t.TeacherLesson)
                    .ThenInclude(tl => tl.Lesson)
                    .Where(p => p.TeacherLesson.Any(pc => pc.Lesson.Url == lesson));
            }
            return await teachers.ToListAsync();
        }

        public async Task<List<Teacher>> GetTeachersWithUser()
        {
            return await OzelDersContext
                .Teachers
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<Teacher> GetTeacherWithLessons(int id)
        {
            return await OzelDersContext
                .Teachers
                .Where(p => p.Id == id)
                .Include(p => p.TeacherLesson)
                .ThenInclude(pc => pc.Lesson)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateTeacherAsync(Teacher teacher, int[] selectedLessonIds)
        {
            Teacher newTeacher = await OzelDersContext
                .Teachers
                .Include(t => t.TeacherLesson)
                .FirstOrDefaultAsync(t => t.Id == teacher.Id);
            newTeacher.TeacherLesson = selectedLessonIds
                .Select(lesId => new TeacherLesson
                {
                    TeacherId = newTeacher.Id,
                    LessonId = lesId
                }).ToList();
            OzelDersContext.Update(newTeacher);
            await OzelDersContext.SaveChangesAsync();
        }
    }
}
