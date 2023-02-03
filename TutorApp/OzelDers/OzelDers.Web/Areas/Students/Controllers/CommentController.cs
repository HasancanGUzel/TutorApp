using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Business.Concrete;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using OzelDers.Web.Areas.Students.Models.Dtos;

namespace OzelDers.Web.Areas.Students.Controllers
{
    [Authorize(Roles = "Student")]


    [Area("Students")]
    public class CommentController : Controller
    {

        private readonly ICommentService _commentService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userService;

        public CommentController(ICommentService commentService, IStudentService studentService, ITeacherService teacherService, UserManager<User> userService)
        {
            _commentService = commentService;
            _studentService = studentService;
            _teacherService = teacherService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentAddDto commentAddDto)
        {
            var userId = _userService.GetUserId(User);

            Comment comment = new Comment
            {
                UserId = userId,
                Content = commentAddDto.Content,
                DateAdded = DateTime.Now,
                TeacherId = commentAddDto.TeacherId,


            };
            await _commentService.CreateAsync(comment);
            var teacher = await _teacherService.GetByIdAsync(comment.TeacherId);

            return RedirectToAction("TeacherDetails", "Home", new { teacherid = teacher.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            var teacher = await _teacherService.GetByIdAsync(comment.TeacherId);
            if (comment == null)
            {
                return NotFound();
            }
            _commentService.Delete(comment);
            return RedirectToAction("TeacherDetails", "Home", new { teacherid = teacher.Id });
        }



    }
}
