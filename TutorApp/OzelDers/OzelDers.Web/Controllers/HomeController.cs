using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Web.Models.Dtos;

namespace OzelDers.Web.Controllers;

public class HomeController : Controller
{
    private readonly ITeacherService _teacherManager;

    public HomeController(ITeacherService teacherManager)
    {
        _teacherManager = teacherManager;
    }

    public async Task<IActionResult> Index()
    {
        List<Teacher> teachers = await _teacherManager.GetHomePageTeachersAsync();
        List<TeacherListDto> teacherListDtos = new List<TeacherListDto>();
        foreach (var teacher in teachers)
        {
            teacherListDtos.Add(new TeacherListDto
            {
                Id=teacher.Id,
                FirstName=teacher.FirstName,
                LastName=teacher.LastName,
                Price=teacher.Price,
                ImageUrl=teacher.ImageUrl,
                Experience=teacher.Experience,
                City=teacher.City,
                Location=teacher.Location,
                Gender =teacher.Gender,
               

            });
        }
        return View(teacherListDtos);
    }


    public async Task<IActionResult> Search( string searchString)
    {
        List<Teacher> searchResults = await _teacherManager.GetSearchResultsAsync(searchString);
        List<TeacherListDto> teacherDtos = new List<TeacherListDto>();
        foreach (var teacher in searchResults)
        {
            teacherDtos.Add(new TeacherListDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Price = teacher.Price,
                ImageUrl = teacher.ImageUrl,
                Experience = teacher.Experience,
                City = teacher.City,
                Location = teacher.Location,
                Gender = teacher.Gender,
            });
        }
        return View(teacherDtos);
    }


}
