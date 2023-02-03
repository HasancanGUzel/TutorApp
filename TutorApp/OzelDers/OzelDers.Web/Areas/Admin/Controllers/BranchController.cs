using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Web.Areas.Admin.Models.Dtos;
using OzelDers.Core;
using Microsoft.AspNetCore.Authorization;

namespace OzelDers.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BranchController : Controller
    {
      
        private readonly IBranchService _branchService;
        private readonly ILessonService _lessonService;

        public BranchController(IBranchService branchService, ILessonService lessonService)
        {
            _branchService = branchService;
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index()
        {
            var branchs = await _branchService.GetBranchsWithLessons();
            var branchListDto = branchs
                .Select(b => new BranchListDto
                {
                    Branch = b
                }).ToList();
            ViewBag.SelectedMenu = "Branch";
            ViewBag.Title = "Branşlar";
            return View(branchListDto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var lessons = await _lessonService.GetAllAsync();
            var branchAddDto = new BranchAddDto
            {
                Lessons = lessons
            };
            return View(branchAddDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BranchAddDto branchAddDto)
        {
            if (ModelState.IsValid)
            {
                var url = Jobs.InitUrl(branchAddDto.Name);
                var branch = new Branch 
                {
                    Name = branchAddDto.Name,
                    Url = url,
                 
                };
                await _branchService.CreateBranchAsync(branch, branchAddDto.SelectedLessonIds); 
                return RedirectToAction("Index");

            }
            
            var lessons = await _lessonService.GetAllAsync();
            branchAddDto.Lessons = lessons;
            return View(branchAddDto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var lessons = await _lessonService.GetAllAsync();
            var branch = await _branchService.GetBranchWithLessons(id);
            var url = Jobs.InitUrl(branch.Name);

            BranchUpdateDto branchUpdateDto = new BranchUpdateDto
            {

                Id = branch.Id,
                Name = branch.Name,
              
                Url = url,
               
                Lessons = lessons,
                SelectedLessonIds = branch.LessonBranch.Select(pc => pc.LessonId).ToArray()
               
            };

            return View(branchUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BranchUpdateDto branchUpdateDto, int[] selectedLessonIds)
        {
            if (ModelState.IsValid)
            {
                var branch = await _branchService.GetByIdAsync(branchUpdateDto.Id);
                if (branch == null)
                {
                    return NotFound();
                }
                var url = Jobs.InitUrl(branch.Name);

                branch.Name = branchUpdateDto.Name;
               
                branchUpdateDto.Url = Jobs.InitUrl(branchUpdateDto.Name);

                branch.Url = url;
                await _branchService.UpdateBranchAsync(branch, selectedLessonIds);



                _branchService.Update(branch);
                return RedirectToAction("Index");



            }
            var lessons = await _lessonService.GetAllAsync();
            branchUpdateDto.Lessons = lessons;
            return View(branchUpdateDto);
        }



        public async Task<IActionResult> Delete(int id)  
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            _branchService.Delete(branch); 
            return RedirectToAction("Index"); 
        }
    }
}
