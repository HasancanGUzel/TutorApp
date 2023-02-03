using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OzelDers.Business.Abstract;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using OzelDers.Web.EmailServices.Abstract;
using OzelDers.Web.Models.Dtos;
using OzelDers.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace OzelDers.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IBranchService _branchManager;
        private readonly ILessonService _lessonManager;
        private readonly ITeacherService _teacherManager;
        private readonly IStudentService _studentManager;



        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, IBranchService branchManager, ITeacherService teacherManager, IStudentService studentManager, ILessonService lessonManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _branchManager = branchManager;
            _teacherManager = teacherManager;
            _studentManager = studentManager;
            _lessonManager = lessonManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult RegisterMenu()
        {
            return View();
        }

        public async Task<IActionResult> Teacher()
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
            var registerDto = new RegisterDto
            {
                Lessons = lessons,
                Branches = branches,
                GenderSelectList = genderList,
                CityList = cityList,
                IlceList = ilceList,
                DeneyimList = deneyimList
            };



            return View(registerDto);
        }

        [HttpPost]

        public async Task<IActionResult> Teacher(RegisterDto registerDto)
        {

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.Phone
                };
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {


                    var teacher = new Teacher
                    {
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        ImageUrl = Jobs.UploadImage(registerDto.ImageFile),
                        Price = registerDto.Price,
                        Location = registerDto.Location,
                        City = registerDto.City,
                        Experience = registerDto.Experience,
                        Address = registerDto.Adress,
                        Gender = registerDto.Gender,
                        About = registerDto.About,
                        BranchId = registerDto.SelectedBranchIds,
                        IsHome = registerDto.IsHome,
                        UserId = user.Id
                    };
                    await _teacherManager.CreateTeacherAsync(teacher, registerDto.SelectedLessonIds);//burada createTeacher kullanmayı unutma
                    var RoleAdd = await _userManager.AddToRoleAsync(user, "Teacher");



                    return RedirectToAction("Login", "Account");
                }

            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu lütfen tekrar deneyiniz");
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

            registerDto.Lessons = lessons;
            registerDto.Branches = branches;
            registerDto.GenderSelectList = genderList;
            registerDto.CityList = cityList;
            registerDto.IlceList = ilceList;
            registerDto.DeneyimList = deneyimList;




            return View(registerDto);
        }


        //------------------------------------------------
        public async Task<IActionResult> Student()
        {
            #region Gender
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
                Selected = true
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
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
            });









            #endregion

            var lessons = await _lessonManager.GetAllAsync();
            var studentDto = new StudentDto
            {
                Lessons = lessons,
                GenderSelectList = genderList,
                CityList = cityList,
                IlceList = ilceList,

            };
            return View(studentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Student(StudentDto studentDto)
        {

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = studentDto.UserName,
                    Email = studentDto.Email,
                    PhoneNumber = studentDto.Phone
                };
                var result = await _userManager.CreateAsync(user, studentDto.Password);

                if (result.Succeeded)
                {

                    var student = new Student
                    {
                        FirstName = studentDto.FirstName,
                        LastName = studentDto.LastName,
                        ImageUrl = Jobs.UploadImage(studentDto.ImageFile),
                        Gender = studentDto.Gender,
                        City = studentDto.City,
                        Location = studentDto.Location,
                        Address = studentDto.Adress,
                        About = studentDto.About,
                        IsHome = studentDto.IsHome,
                        UserId = user.Id
                    };
                    await _studentManager.CreateStudentAsync(student, studentDto.SelectedLessonIds);
                    var RoleAdd = await _userManager.AddToRoleAsync(user, "Student");


                    return RedirectToAction("Login", "Account");

                }

            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu lütfen tekrar deneyiniz");
            #region Gender
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
                Selected = true
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
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
            });
            ilceList.Add(new SelectListItem
            {
                Text = "Esenyurt",
                Value = "Esenyurt",
            });









            #endregion

            var lessons = await _lessonManager.GetAllAsync();

            studentDto.Lessons = lessons;
            studentDto.GenderSelectList = genderList;
            studentDto.CityList = cityList;
            studentDto.IlceList = ilceList;


            return View(studentDto);
        }



        //------------------------------------------------

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginDto { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı!");
                    return View(loginDto);
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
                if (result.Succeeded)
                {
                    if (loginDto.ReturnUrl == null)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        if (userRoles[0]=="Teacher")
                        {
                            return Redirect("~/Teachers/Home");
                        }
                        else if (userRoles[0]=="Student")
                        {
                            return Redirect("~/Students/Home");
                        }


                        return Redirect($"~/");
                    }


                }
                ModelState.AddModelError("", "Email adresi ya da parola hatalı!");
            }
            return View(loginDto);
        }



        //------------------------------------------------


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




        //-------------------------------------------------


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                TempData["Message"] = Jobs.CreateMessage("Hata", "Lütfen mail adresinizi eksiksiz bir şekild giriniz.", "danger");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["Message"] = Jobs.CreateMessage("Hata", "Böyle kayıtlı bir mail adresi bulunamadı. Lütfen kontrol ederek, yeniden deneyiniz.", "danger");
                return View();
            }
            var tokenCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = tokenCode
            });
            await _emailSender.SendEmailAsync(
                email,
                "ShoppingApp Şifre Sıfırlama Linki",
                $"Lütfen parolanızı yenilemek için <a href='https://localhost:7215{url}'>tıklayınız.</a>"
                );
            TempData["Message"] = Jobs.CreateMessage("Bilgi", "Şifre sıfırlama linkiniz, mail adresinize gönderilmiştir. Lütfen mail adresinizi kontrol ediniz.", "info");
            return RedirectToAction("Login", "Account");
        }



        //------------------------------------------------



        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["Message"] = Jobs.CreateMessage("Hata", "Bir sorun oluştu, lütfen daha sonra yeniden deneyiniz.", "danger");
                return RedirectToAction("Index", "Home");
            }
            var resetPasswordDto = new ResetPasswordDto
            {
                Token = token
            };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordDto);
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                TempData["Message"] = Jobs.CreateMessage("Hata", "Böyle bir kullanıcı bulunamadı. Tekrar deneyiniz", "danger");
                return View(resetPasswordDto);
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (result.Succeeded)
            {
                TempData["Message"] = Jobs.CreateMessage("Başarılı", "Parolanız başarıyla değiştirilmiştir. Giriş yapmayı deneyebilirsiniz.", "success");
                return RedirectToAction("Login", "Account");
            }
            TempData["Message"] = Jobs.CreateMessage("Hata", "Bir hata oluştu. Lütfen admin ile iletişime geçiniz.", "danger");
            return Redirect("~/");
        }


        //------------------------------------------------





        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["Message"] = Jobs.CreateMessage("Hata", "Geçersiz token ya da user bilgisi.", "danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["Message"] = Jobs.CreateMessage("Başarılı", "Hesabınız başarıyla onaylandı. Giriş yapabilirsiniz.", "success");
                    return RedirectToAction("Login", "Account");
                }

            }
            TempData["Message"] = Jobs.CreateMessage("Hata", "Bir sorun oluştu ve hesabınız onaylanmadı. Lütfen admin ile iletişime geçiniz.", "danger");
            return View();
        }

        //-------------------------------------

        public async Task<IActionResult> Manage(string id)
        {
            var name = id;
            if (String.IsNullOrEmpty(name))
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(name);
            if (user == null) { return NotFound(); }

            List<Teacher> teachers = await _teacherManager.GetAllAsync();
            List<Student> students = await _studentManager.GetAllAsync();




            foreach (var teacher in teachers)
            {
                if (teacher.UserId == user.Id)
                {
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

                    UserManageDto userManageDto = new UserManageDto
                    {
                        Id = user.Id,
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        Gender = teacher.Gender,
                        DateOfBirth = teacher.DateOfBirth,
                        ImageUrl = teacher.ImageUrl,
                        Price = teacher.Price,
                        Adress = teacher.Address,
                        UserName = user.UserName,
                        Email = user.Email,
                        About = teacher.About,
                        GenderSelectList = genderList,
                        CityList = cityList,
                        IlceList = ilceList,
                        DeneyimList = deneyimList,
                        IsHome=teacher.IsHome,
                        Branches = await _branchManager.GetAllAsync(),
                        SelectedBranchIds = teacher.BranchId,



                        Lessons = await _lessonManager.GetAllAsync(),
                        SelectedLessonIds = teacherswith.TeacherLesson.Select(pc => pc.LessonId).ToArray(),
                        Tip = "teacher",
                        

                    };
                    ViewBag.Tip = "teacher";
                    return View(userManageDto);
                }

            }
            foreach (var student in students)
            {
                if (student.UserId == user.Id)
                {
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

                    var studentwith = await _studentManager.GetStudentWithLessons(student.Id);
                    UserManageDto userManageDto = new UserManageDto
                    {
                        Id = user.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Gender = student.Gender,
                        DateOfBirth = student.DateOfBirth,
                        UserName = user.UserName,
                        Adress = student.Address,
                        Email = user.Email,
                        About = student.About,
                        GenderSelectList = genderList,
                        CityList = cityList,
                        IlceList = ilceList,
                        ImageUrl = student.ImageUrl,
                        IsHome = student.IsHome,

                        Lessons = await _lessonManager.GetAllAsync(),
                        SelectedLessonIds = studentwith.StudentLesson.Select(pc => pc.LessonId).ToArray(),

                        Tip = "student"

                    };
                    ViewBag.Tip = "student";
                    return View(userManageDto);
                }


            }
            TempData["Message"] = Jobs.CreateMessage("Hata", "Bir hata oluştu. Lütfen admin ile iletişime geçiniz.", "danger");
            return Redirect("~/");


        }

        [HttpPost]
        public async Task<IActionResult> Manage(UserManageDto userManageDto, int[] selectedLessonIds)
        {
            List<Teacher> teachers = await _teacherManager.GetAllAsync();
            List<Student> students = await _studentManager.GetAllAsync();
            if (userManageDto == null) { return NotFound(); }
            var user = await _userManager.FindByIdAsync(userManageDto.Id);
            if (user == null) { return NotFound(); }
            if (userManageDto.Tip == "teacher")
            {

                foreach (var teacher in teachers)
                {
                    if (teacher.UserId == user.Id)
                    {
                        var imageUrl = userManageDto.ImageFile != null ? Jobs.UploadImage(userManageDto.ImageFile) : teacher.ImageUrl;
                        user.UserName = userManageDto.UserName;
                        user.Email = userManageDto.Email;
                        teacher.FirstName = userManageDto.FirstName;
                        teacher.LastName = userManageDto.LastName;
                        teacher.DateOfBirth = userManageDto.DateOfBirth;
                        teacher.ImageUrl = imageUrl;
                        teacher.Price = userManageDto.Price;
                        teacher.Address = userManageDto.Adress;
                        teacher.Gender = userManageDto.Gender;
                        teacher.About = userManageDto.About;
                        teacher.City = userManageDto.City;
                        teacher.IsHome = userManageDto.IsHome;
                        teacher.Location = userManageDto.Location;
                        teacher.Experience = userManageDto.Experience;
                        teacher.BranchId = userManageDto.SelectedBranchIds;
                        var userResult = await _userManager.UpdateAsync(user);
                        await _teacherManager.UpdateTeacherAsync(teacher, selectedLessonIds);
                        if (userResult.Succeeded)
                        {
                            TempData["Message"] = Jobs.CreateMessage("Başarılı!", "Profiliniz başarıyla kaydedilmiştir.", "success");
                            return RedirectToAction("Index", "Home", new { area = "Teachers" });


                        }
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
                        userManageDto.GenderSelectList = genderList;
                        userManageDto.CityList = cityList;
                        userManageDto.IlceList = ilceList;
                        userManageDto.DeneyimList = deneyimList;
                        return View(userManageDto);
                    }
                }
                
            }
            if (userManageDto.Tip == "student")
            {

                foreach (var student in students)
                {
                    if (student.UserId == user.Id)
                    {
                        user.UserName = userManageDto.UserName;
                        user.Email = userManageDto.Email;
                        student.FirstName = userManageDto.FirstName;
                        student.LastName = userManageDto.LastName;
                        student.DateOfBirth = userManageDto.DateOfBirth;
                        student.Address = userManageDto.Adress;
                        student.Gender = userManageDto.Gender;
                        student.About = userManageDto.About;
                        student.IsHome = userManageDto.IsHome;

                        student.City = userManageDto.City;
                        student.Location = userManageDto.Location;
                        var userResult = await _userManager.UpdateAsync(user);
                        await _studentManager.UpdateStudentAsync(student, selectedLessonIds);
                        //await _studentManager.UpdateAsyncc(student);
                        if (userResult.Succeeded)
                        {
                            TempData["Message"] = Jobs.CreateMessage("Başarılı!", "Profiliniz başarıyla kaydedilmiştir.", "success");
                            return RedirectToAction("Index", "Home", new { area = "Students" });

                        }
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
                        userManageDto.GenderSelectList = genderList;
                        userManageDto.CityList = cityList;
                        userManageDto.IlceList = ilceList;
                        return View(userManageDto);
                    }
                }
                return RedirectToAction("Manage");
            }

            TempData["Message"] = Jobs.CreateMessage("Hata", "Bir hata oluştu. Lütfen admin ile iletişime geçiniz.", "danger");
            return Redirect("~/");

        }




        [NonAction]
        private List<SelectListItem> FillIsBranchList()
        {
            List<SelectListItem> branch = new List<SelectListItem>();
            branch.Add(new SelectListItem
            {
                Text = "Branşlar",
                Value = "null"
            });
            return branch;
        }

        [NonAction]
        private List<SelectListItem> FillIsLessonList()
        {
            List<SelectListItem> lesson = new List<SelectListItem>();
            lesson.Add(new SelectListItem
            {
                Text = "Dersler",
                Value = "null"
            });
            return lesson;
        }
    }
}
