using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using OzelDers.Web.Areas.Students.Models.Dtos;


namespace OzelDers.Web.Areas.Students.Controllers
{
    [Authorize(Roles = "Student")]


    [Area("Students")]
    public class HomeController : Controller
    {

        private readonly ITeacherService _teacherManager;
        private readonly ICommentService _commentManager;
        private readonly IStudentService _studentManager;
        private readonly UserManager<User> _userManager;


        public HomeController(ITeacherService teacherManager, ICommentService commentManager, IStudentService studentManager, UserManager<User> userManager)
        {
            _teacherManager = teacherManager;
            _commentManager = commentManager;
            _studentManager = studentManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _teacherManager.GetHomePageTeachersAsync();


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
                   

                });
            }
            return View(teacherListDtos);
        }

        public async Task<IActionResult> TeacherDetails(int teacherid)
        {
            if (teacherid == null)
            {
                return NotFound();
            }
            var teacher = await _teacherManager.GetTeacherDetailsByIdAsync(teacherid);
            var comments = await _commentManager.GetByTeacherId(teacher.Id);

            var userId = _userManager.GetUserId(User);


            TeacherDetailsDto teacherDetailsDto = new TeacherDetailsDto
            {
                Id = teacher.Id,
                UserId = userId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Price = teacher.Price,
                ImageUrl = teacher.ImageUrl,
                Experience = teacher.Experience,
                City = teacher.City,
                Location = teacher.Location,
                Gender = teacher.Gender,
                Branch = teacher.Branch,
                Phone = teacher.User.PhoneNumber,
                Comments = comments != null ? comments : new List<Comment>(),
                Lessons = teacher
                    .TeacherLesson
                    .Select(pc => pc.Lesson)
                    .ToList()
            };

            return View(teacherDetailsDto);
        }


    }
}
