using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Web.Areas.Teachers.Models.Dtos;

namespace OzelDers.Web.Areas.Teachers.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teachers")]
    public class HomeController : Controller
    {

        private readonly IStudentService _studentManager;

        public HomeController(IStudentService studentManager)
        {
            _studentManager = studentManager;
        }
        public async Task<IActionResult> Index()
        {

            List<Student> students = await _studentManager.GetHomePageStudentsAsync();


            List<StudentListDto> studentListDtos = new List<StudentListDto>();
            foreach (var student in students)
            {
                studentListDtos.Add(new StudentListDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ImageUrl = student.ImageUrl,
                    City = student.City,
                    Gender = student.Gender,


                });
            }
            return View(studentListDtos);
        }

        public async Task<IActionResult> StudentsDetails(int studentid)
        {
            if (studentid == null)
            {
                return NotFound();
            }
            var student = await _studentManager.GetStudentDetailsByIdAsync(studentid);
            StudentDetailsDto studentDetailsDto = new StudentDetailsDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ImageUrl = student.ImageUrl,
                City = student.City,
                Gender = student.Gender,
                Lessons = student
                    .StudentLesson
                    .Select(pc => pc.Lesson)
                    .ToList()

            };

            return View(studentDetailsDto);
        }

        public async Task<IActionResult> StudentList(string studentlessonurl)
        {
            List<Student> students = await _studentManager.GetStudentsByLessonAsync(studentlessonurl);
            List<StudentListDto> studentListDtos = new List<StudentListDto>();
            foreach (var student in students)
            {
                studentListDtos.Add(new StudentListDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,

                    ImageUrl = student.ImageUrl,

                    City = student.City,
                    Gender = student.Gender,
                    Location = student.Location,


                });
            }
            return View(studentListDtos);
        }
    }
}
