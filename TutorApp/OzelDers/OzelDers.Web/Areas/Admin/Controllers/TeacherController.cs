using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OzelDers.Business.Abstract;
using OzelDers.Business.Concrete;
using OzelDers.Core;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using OzelDers.Web.Areas.Admin.Models.Dtos;
using OzelDers.Web.Models.Dtos;

namespace OzelDers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITeacherService _teacherManager;
        private readonly IBranchService _branchManager;
        private readonly ILessonService _lessonManager;

        public TeacherController(UserManager<User> userManager, RoleManager<Role> roleManager, ITeacherService teacherManager, IBranchService branchManager, ILessonService lessonManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _teacherManager = teacherManager;
            _branchManager = branchManager;
            _lessonManager = lessonManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _teacherManager.GetTeachersWithUser();
            List<TeacherDto> teacherListDto = new List<TeacherDto>();
            foreach (var teacher in teachers)
            {
                teacherListDto.Add(new TeacherDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    UserName = teacher.User.UserName,
                    Email = teacher.User.Email,
                    UserId = teacher.UserId


                });
            }
            ViewBag.SelectedMenu = "Teacher";
            ViewBag.Title = "Öğretmenler";
            return View(teacherListDto);
        }


        public async Task<IActionResult> Create()
        {



            #region genderList
            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem
            {
                Text = "Kadın",
                Value = "Kadın",
                Selected = true

            });
            genderList.Add(new SelectListItem
            {
                Text = "Erkek",
                Value = "Erkek",


            });
            #endregion


            #region CityList
            List<SelectListItem> cityList = new List<SelectListItem>();

            cityList.Add(new SelectListItem
            {
                Text = "İstanbul",
                Value = "Istanbul",
            });

            #endregion

            #region İlçeList
            List<SelectListItem> ilceList = new List<SelectListItem>();
            ilceList.Add(new SelectListItem
            {
                Text = "Adalar",
                Value = "Adalar",
                Selected = true

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Arnavutköy",
                Value = "Arnavutköy",
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Ataşehir",
                Value = "Ataşehir",
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Avcılar",
                Value = "Avcılar",

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
            });




            #endregion

            #region DeneyimList
            List<SelectListItem> deneyimList = new List<SelectListItem>();
            deneyimList.Add(new SelectListItem
            {
                Text = "1-3 Yıl Tecrübeli",
                Value = "1-3 Yıl Tecrübeli",
                Selected = true
            });
            deneyimList.Add(new SelectListItem
            {
                Text = "3-5 Yıl Tecrübeli",
                Value = "3-5 Yıl Tecrübeli"

            });
            deneyimList.Add(new SelectListItem
            {
                Text = "5-10 Yıl Tecrübeli",
                Value = "5-10 Yıl Tecrübeli"

            });
            deneyimList.Add(new SelectListItem
            {
                Text = "10+ Yıl Tecrübeli",
                Value = "10+ Yıl Tecrübeli"

            });
            #endregion

            var branches = await _branchManager.GetAllAsync();
            var lessons = await _lessonManager.GetAllAsync();




            TeacherDto teacherDto = new TeacherDto
            {
                Lessons = lessons,
                Branches = branches,
                GenderSelectList = genderList,
                CityList = cityList,
                IlceList = ilceList,
                DeneyimList = deneyimList
            };




            ViewBag.Title = "Öğretmen Oluştur";
            return View(teacherDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = teacherDto.UserName,
                    Email = teacherDto.Email,
                    PhoneNumber = teacherDto.Phone,
                };
                var result = await _userManager.CreateAsync(user, "Qwe123.");
                if (result.Succeeded)
                {
                    var teacher = new Teacher
                    {
                        FirstName = teacherDto.FirstName,
                        LastName = teacherDto.LastName,
                        ImageUrl = Jobs.UploadImage(teacherDto.ImageFile),
                        Price = teacherDto.Price,
                        Location = teacherDto.Location,
                        City = teacherDto.City,
                        Experience = teacherDto.Experience,
                        DateOfBirth = teacherDto.DateOfBirth,
                        Address = teacherDto.Adress,
                        Gender = teacherDto.Gender,
                        About = teacherDto.About,
                        BranchId = teacherDto.SelectedBranchIds,
                        UserId = user.Id,
                        IsHome=teacherDto.IsHome
                       
                    };
                    await _teacherManager.CreateTeacherAsync(teacher, teacherDto.SelectedLessonIds);//burada createTeacher kullanmayı unutma
                    var RoleAdd = await _userManager.AddToRoleAsync(user, "Teacher");
                    TempData["Message"] = Jobs.CreateMessage("Başarılı", $"{user.UserName} kullanıcısı başarıyla oluşturuldu.", "success");

                    return RedirectToAction("Index", "Teacher");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            #region genderList
            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem
            {
                Text = "Kadın",
                Value = "Kadın",
                Selected = true

            });
            genderList.Add(new SelectListItem
            {
                Text = "Erkek",
                Value = "Erkek",


            });
            #endregion


            #region CityList
            List<SelectListItem> cityList = new List<SelectListItem>();

            cityList.Add(new SelectListItem
            {
                Text = "İstanbul",
                Value = "Istanbul",
            });

            #endregion

            #region İlçeList
            List<SelectListItem> ilceList = new List<SelectListItem>();
            ilceList.Add(new SelectListItem
            {
                Text = "Adalar",
                Value = "Adalar",
                Selected = true

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Arnavutköy",
                Value = "Arnavutköy",
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Ataşehir",
                Value = "Ataşehir",
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Avcılar",
                Value = "Avcılar",

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
            });




            #endregion

            #region DeneyimList
            List<SelectListItem> deneyimList = new List<SelectListItem>();
            deneyimList.Add(new SelectListItem
            {
                Text = "1-3 Yıl Tecrübeli",
                Value = "1-3 Yıl Tecrübeli",
                Selected = true
            });
            deneyimList.Add(new SelectListItem
            {
                Text = "3-5 Yıl Tecrübeli",
                Value = "3-5 Yıl Tecrübeli"

            });
            deneyimList.Add(new SelectListItem
            {
                Text = "5-10 Yıl Tecrübeli",
                Value = "5-10 Yıl Tecrübeli"

            });
            deneyimList.Add(new SelectListItem
            {
                Text = "10+ Yıl Tecrübeli",
                Value = "10+ Yıl Tecrübeli"

            });
            #endregion

            var branches = await _branchManager.GetAllAsync();
            var lessons = await _lessonManager.GetAllAsync();
            teacherDto.Lessons = lessons;
            teacherDto.Branches = branches;
            teacherDto.GenderSelectList = genderList;
            teacherDto.CityList = cityList;
            teacherDto.IlceList = ilceList;
            teacherDto.DeneyimList = deneyimList;

            return View(teacherDto);
        }



        public async Task<IActionResult> Edit(string id)
        {




            var teacher = await _teacherManager.FindByIdTeacherAsync(id);
            if (teacher == null) { return NotFound(); }


            #region gender
            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem
            {
                Text = "Kadın",
                Value = "Kadın",
                Selected = teacher.Gender == "Kadın" ? true : false
            });
            genderList.Add(new SelectListItem
            {
                Text = "Erkek",
                Value = "Erkek",
                Selected = teacher.Gender == "Erkek" ? true : false
            });
            #endregion

            #region CityList
            List<SelectListItem> cityList = new List<SelectListItem>();
            cityList.Add(new SelectListItem
            {
                Text = "İstanbul",
                Value = "Istanbul",
                Selected = true
            });
            #endregion

            #region İlçeList
            List<SelectListItem> ilceList = new List<SelectListItem>();
            ilceList.Add(new SelectListItem
            {
                Text = "Adalar",
                Value = "Adalar",
                Selected = teacher.Location == "Adalar" ? true : false

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Arnavutköy",
                Value = "Arnavutköy",
                Selected = teacher.Location == "Arnavutköy" ? true : false
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Ataşehir",
                Value = "Ataşehir",
                Selected = teacher.Location == "Ataşehir" ? true : false

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Avcılar",
                Value = "Avcılar",
                Selected = teacher.Location == "Avcılar" ? true : false


            });
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
                Selected = teacher.Location == "Esenyurt" ? true : false

            });

            #endregion

            #region DeneyimList
            List<SelectListItem> deneyimList = new List<SelectListItem>();
            deneyimList.Add(new SelectListItem
            {
                Text = "1-3 Yıl Tecrübeli",
                Value = "1-3 Yıl Tecrübeli",
                Selected = teacher.Experience == "1-3 Yıl Tecrübeli" ? true : false
            });
            deneyimList.Add(new SelectListItem
            {
                Text = "3-5 Yıl Tecrübeli",
                Value = "3-5 Yıl Tecrübeli",
                Selected = teacher.Experience == "3-5 Yıl Tecrübeli" ? true : false


            });
            deneyimList.Add(new SelectListItem
            {
                Text = "5-10 Yıl Tecrübeli",
                Value = "5-10 Yıl Tecrübeli",
                Selected = teacher.Experience == "5-10 Yıl Tecrübeli" ? true : false


            });
            deneyimList.Add(new SelectListItem
            {
                Text = "10+ Yıl Tecrübeli",
                Value = "10+ Yıl Tecrübeli",
                Selected = teacher.Experience == "10+ Yıl Tecrübeli" ? true : false


            });
            #endregion

            var teacherswith = await _teacherManager.GetTeacherWithLessons(teacher.Id);

            TeacherDto teacherDto = new TeacherDto
            {
                UserId = teacher.UserId,
                Id = teacher.Id,
                IsHome=teacher.IsHome,
                Phone = teacher.User.PhoneNumber,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Gender = teacher.Gender,
                DateOfBirth = teacher.DateOfBirth,
                ImageUrl = teacher.ImageUrl,
                Price = teacher.Price,
                Adress = teacher.Address,
                UserName = teacher.User.UserName,
                Email = teacher.User.Email,
                About = teacher.About,
                GenderSelectList = genderList,
                CityList = cityList,
                IlceList = ilceList,
                DeneyimList = deneyimList,
                Branches = await _branchManager.GetAllAsync(),
                SelectedBranchIds = teacher.BranchId,
                Lessons = await _lessonManager.GetAllAsync(),
                SelectedLessonIds = teacherswith.TeacherLesson.Select(pc => pc.LessonId).ToArray(),
            };

            return View(teacherDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeacherDto teacherDto, int[] selectedLessonIds)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _teacherManager.FindByIdTeacherAsync(teacherDto.UserId);
                if (teacher == null) { return NotFound(); }
                teacher.FirstName = teacherDto.FirstName;
                teacher.LastName = teacherDto.LastName;
                teacher.User.Email = teacherDto.Email;
                teacher.User.PhoneNumber = teacherDto.Phone;


                var imageUrl = teacherDto.ImageFile != null ? Jobs.UploadImage(teacherDto.ImageFile) : teacher.ImageUrl;


                teacher.DateOfBirth = teacherDto.DateOfBirth;
                teacher.ImageUrl = imageUrl;
                teacher.Price = teacherDto.Price;
                teacher.Address = teacherDto.Adress;
                teacher.Gender = teacherDto.Gender;
                teacher.About = teacherDto.About;
                teacher.City = teacherDto.City;
                teacher.Location = teacherDto.Location;
                teacher.Experience = teacherDto.Experience;
                teacher.BranchId = teacherDto.SelectedBranchIds;
                teacher.IsHome = teacherDto.IsHome;


                var result = await _userManager.UpdateAsync(teacher.User);
                await _teacherManager.UpdateTeacherAsync(teacher, selectedLessonIds);
                if (!result.Succeeded) { return NotFound(); }



                TempData["Message"] = Jobs.CreateMessage("Başarılı", $"{teacher.User.UserName} kullanıcısı başarıyla güncellendi.", "success");
                return RedirectToAction("Index", "Teacher");
            }

            return View(teacherDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var teacher = await _teacherManager.FindByIdTeacherAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            _teacherManager.Delete(teacher);
            await _userManager.DeleteAsync(teacher.User);
            return RedirectToAction("Index");
        }



    }
}
