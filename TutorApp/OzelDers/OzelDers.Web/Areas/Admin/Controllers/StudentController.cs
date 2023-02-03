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

namespace OzelDers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IStudentService _studentManager;
        private readonly IBranchService _branchManager;
        private readonly ILessonService _lessonManager;

        public StudentController(UserManager<User> userManager, RoleManager<Role> roleManager, IBranchService branchManager, ILessonService lessonManager, IStudentService studentManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _branchManager = branchManager;
            _lessonManager = lessonManager;
            _studentManager = studentManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Student> students = await _studentManager.GetStudentsWithUser();
            List<StudentDto> studentListDto = new List<StudentDto>();
            foreach (var student in students)
            {
                studentListDto.Add(new StudentDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    UserName = student.User.UserName,
                    Email = student.User.Email,
                    UserId = student.UserId


                });
            }
            ViewBag.SelectedMenu = "Student";
            ViewBag.Title = "Öğrenciler";
            return View(studentListDto);
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




            var lessons = await _lessonManager.GetAllAsync();


            StudentDto studentAddDto = new StudentDto
            {
                Lessons = lessons,
                GenderSelectList = genderList,
                CityList = cityList,
                IlceList = ilceList,

            };



            ViewBag.Title = "Öğretmen Oluştur";
            return View(studentAddDto);
        }



        [HttpPost]
        public async Task<IActionResult> Create(StudentDto studentAddDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {

                    UserName = studentAddDto.UserName,
                    Email = studentAddDto.Email,


                };
                var result = await _userManager.CreateAsync(user, "Qwe123.");
                if (result.Succeeded)
                {



                    var student = new Student
                    {
                        FirstName = studentAddDto.FirstName,
                        LastName = studentAddDto.LastName,
                        ImageUrl = Jobs.UploadImage(studentAddDto.ImageFile),
                        Location = studentAddDto.Location,
                        City = studentAddDto.City,
                        DateOfBirth = studentAddDto.DateOfBirth,
                        Address = studentAddDto.Adress,
                        Gender = studentAddDto.Gender,
                        About = studentAddDto.About,
                        UserId = user.Id,
                        IsHome=studentAddDto.IsHome,

                    };
                    await _studentManager.CreateStudentAsync(student, studentAddDto.SelectedLessonIds);//burada createTeacher kullanmayı unutma
                    var RoleAdd = await _userManager.AddToRoleAsync(user, "Student");

                    TempData["Message"] = Jobs.CreateMessage("Başarılı", $"{user.UserName} kullanıcısı başarıyla oluşturuldu.", "success");

                    return RedirectToAction("Index", "Student");
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

            var lessons = await _lessonManager.GetAllAsync();

            studentAddDto.Lessons = lessons;
            studentAddDto.GenderSelectList = genderList;
            studentAddDto.CityList = cityList;
            studentAddDto.IlceList = ilceList;

            return View(studentAddDto);
        }


        public async Task<IActionResult> Edit(string id)
        {




            var student = await _studentManager.FindByIdStudentAsync(id);
            if (student == null) { return NotFound(); }


            #region gender
            List<SelectListItem> genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem
            {
                Text = "Kadın",
                Value = "Kadın",
                Selected = student.Gender == "Kadın" ? true : false
            });
            genderList.Add(new SelectListItem
            {
                Text = "Erkek",
                Value = "Erkek",
                Selected = student.Gender == "Erkek" ? true : false
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
                Selected = student.Location == "Adalar" ? true : false

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Arnavutköy",
                Value = "Arnavutköy",
                Selected = student.Location == "Arnavutköy" ? true : false
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Ataşehir",
                Value = "Ataşehir",
                Selected = student.Location == "Ataşehir" ? true : false

            });
            ilceList.Add(new SelectListItem
            {
                Text = "Avcılar",
                Value = "Avcılar",
                Selected = student.Location == "Avcılar" ? true : false


            });
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
                Selected = student.Location == "Esenyurt" ? true : false

            });

            #endregion



            var studentsWith = await _studentManager.GetStudentWithLessons(student.Id);

            StudentDto studentAddDto = new StudentDto
            {
                UserId = student.UserId,
                Id = student.Id,
                IsHome=student.IsHome,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                ImageUrl = student.ImageUrl,
                Adress = student.Address,
                UserName = student.User.UserName,
                Email = student.User.Email,
                About = student.About,
                GenderSelectList = genderList,
                CityList = cityList,
                IlceList = ilceList,
                Lessons = await _lessonManager.GetAllAsync(),
                SelectedLessonIds = studentsWith.StudentLesson.Select(pc => pc.LessonId).ToArray(),
            };

            return View(studentAddDto);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(StudentDto studentAddDto, int[] selectedLessonIds)
        {
            if (ModelState.IsValid)
            {
                var student = await _studentManager.FindByIdStudentAsync(studentAddDto.UserId);
                if (student == null) { return NotFound(); }
                student.FirstName = studentAddDto.FirstName;
                student.LastName = studentAddDto.LastName;
                student.User.UserName = studentAddDto.UserName;
                student.User.Email = studentAddDto.Email;


                var imageUrl = studentAddDto.ImageFile != null ? Jobs.UploadImage(studentAddDto.ImageFile) : student.ImageUrl;


                student.DateOfBirth = studentAddDto.DateOfBirth;
                student.ImageUrl = imageUrl;
                student.Address = studentAddDto.Adress;
                student.Gender = studentAddDto.Gender;
                student.About = studentAddDto.About;
                student.City = studentAddDto.City;
                student.Location = studentAddDto.Location;
                student.DateOfBirth = studentAddDto.DateOfBirth;
                student.IsHome = studentAddDto.IsHome;


                var result = await _userManager.UpdateAsync(student.User);
                await _studentManager.UpdateStudentAsync(student, selectedLessonIds);
                if (!result.Succeeded) { return NotFound(); }



                TempData["Message"] = Jobs.CreateMessage("Başarılı", $"{student.User.UserName} kullanıcısı başarıyla güncellendi.", "success");
                return RedirectToAction("Index", "Student");
            }


            return View(studentAddDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentManager.FindByIdStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentManager.Delete(student);
            await _userManager.DeleteAsync(student.User);
            return RedirectToAction("Index");
        }
    }
}
