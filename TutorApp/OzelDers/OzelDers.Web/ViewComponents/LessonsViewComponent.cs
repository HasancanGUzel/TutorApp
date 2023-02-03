using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;

namespace OzelDers.Web.ViewComponents
{
    public class LessonsViewComponent: ViewComponent
    {
        private readonly ILessonService _lessonManager;

        public LessonsViewComponent(ILessonService lessonManager)
        {
            _lessonManager = lessonManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (RouteData.Values["lessonurl"] != null)
            {
                ViewBag.SelectedLesson = RouteData.Values["lessonurl"];
            }
            var lessons = await _lessonManager.GetAllAsync();
            return View(lessons);
        }
    }
}
