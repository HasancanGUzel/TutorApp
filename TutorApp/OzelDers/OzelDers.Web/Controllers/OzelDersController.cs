using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Web.Models.Dtos;

namespace OzelDers.Web.Controllers
{
    public class OzelDersController : Controller
    {
        private readonly ITeacherService _teacherManager;

        public OzelDersController(ITeacherService teacherManager)
        {
            _teacherManager = teacherManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TeacherList(string lessonurl)
        {
            List<Teacher> teachers = await _teacherManager.GetTeachersByLessonAsync(lessonurl);
            List<TeacherListDto> teacherListDtos = new List<TeacherListDto>();
            foreach (var teacher in teachers)
            {
                teacherListDtos.Add(new TeacherListDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Price = teacher.Price,
                    ImageUrl = teacher.ImageUrl,
                    Experience = teacher.Experience,
                    City = teacher.City,
                    Gender = teacher.Gender,
                    Location = teacher.Location,       
                    Branches = teachers
                   .Select(tb => tb.Branch)
                   .ToList()

                });
            }
            return View(teacherListDtos);
        }
    }
}
