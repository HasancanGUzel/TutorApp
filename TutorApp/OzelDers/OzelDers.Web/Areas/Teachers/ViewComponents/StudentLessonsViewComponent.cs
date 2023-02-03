using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;

namespace OzelDers.Web.Areas.Teachers.ViewComponents
{
    public class StudentLessonsViewComponent : ViewComponent
    {
        private readonly ILessonService _lessonManager;

        public StudentLessonsViewComponent(ILessonService lessonManager)
        {
            _lessonManager = lessonManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (RouteData.Values["studentlessonurl"] != null)
            {
                ViewBag.SelectedLesson = RouteData.Values["studentlessonurl"];
            }
            var lessons = await _lessonManager.GetAllAsync();
            return View(lessons);
        }
    }
}
