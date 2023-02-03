using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Web.Areas.Admin.Models.Dtos;
using OzelDers.Core;
using System.Xml.Linq;

namespace OzelDers.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]

    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetAllAsync();
            List<LessonListDto> lessonListDto = new List<LessonListDto>();
            foreach (var lesson in lessons)
            {
                lessonListDto.Add(new LessonListDto
                {
                    Id=lesson.Id,
                    Name =lesson.Name,
                    Url=lesson.Url

                });
            }
            ViewBag.SelectedMenu = "Lesson";
            ViewBag.Title = "Dersler";
            return View(lessonListDto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonAddDto lessonAddDto)
        {
            if (ModelState.IsValid)
            {
                var lesson = new Lesson
                {
                    Name = lessonAddDto.Name,
                    Url = Jobs.InitUrl(lessonAddDto.Name)

                };
                await _lessonService.CreateAsync(lesson);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            var lessonUpdateDto = new LessonUpdateDto
            {
                Id = lesson.Id,
                Name = lesson.Name,
                Url = lesson.Url
            };
            return View(lessonUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LessonUpdateDto lessonUpdateDto) 
        {
            if (ModelState.IsValid)
            {
                var lesson = await _lessonService.GetByIdAsync(lessonUpdateDto.Id);
                if (lesson == null)
                {
                    return NotFound();
                }
                lesson.Name = lessonUpdateDto.Name; 
                lesson.Url = Jobs.InitUrl(lessonUpdateDto.Name);


                _lessonService.Update(lesson);
                return RedirectToAction("Index");
            }
            return View(lessonUpdateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)  
        {
            var lesson = await _lessonService.GetByIdAsync(id); 
            if (lesson == null)
            {
                return NotFound();
            }
            _lessonService.Delete(lesson); 
            return RedirectToAction("Index"); 
        }

    }
}

