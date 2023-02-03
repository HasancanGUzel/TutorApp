using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OzelDers.Business.Abstract;
using OzelDers.Core;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using OzelDers.Web.Areas.Admin.Models.Dtos;

namespace OzelDers.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ITeacherService _teacherManager;
        private readonly IStudentService _studentManager;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager, ITeacherService teacherManager , IStudentService studentManager )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _teacherManager = teacherManager;
            _studentManager = studentManager;
        }

        public IActionResult Index()
        {
            List<RoleDto> roles = _roleManager.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            if (ModelState.IsValid) 
            {
                var result = await _roleManager.CreateAsync(new Role 
                {
                    Name = roleDto.Name,
                    Description = roleDto.Description
                });
                if (result.Succeeded)
                {
                    TempData["Message"] = Jobs.CreateMessage("Başarılı", roleDto.Name + " rolü başarı ile eklenmiştir", "success");
                    return RedirectToAction("Index", "Role");
                }
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(roleDto);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var users = _userManager.Users;
            var members = new List<User>();
            var nonMembers = new List<User>();
            foreach (var user in users)
            {
             

               
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
              

            }
            RoleDetailsDto roleDetailsDto = new RoleDetailsDto
            {
                Role = role, 
                Members = members,
                NonMembers = nonMembers
            };
            return View(roleDetailsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditDetailsDto roleEditDetailsDto)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in roleEditDetailsDto.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    var result = await _userManager.AddToRoleAsync(user, roleEditDetailsDto.RoleName);
                    if (!result.Succeeded) 
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                foreach (var userId in roleEditDetailsDto.IdsToRemove ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    var result = await _userManager.RemoveFromRoleAsync(user, roleEditDetailsDto.RoleName);
                    if (!result.Succeeded) 
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                return RedirectToAction("Index", "Role");
            }
            return Redirect("/Admin/Role/Edit/" + roleEditDetailsDto.RoleId);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }



        [NonAction]
        private UserRolesDto GetUserRolesDto()
        {

            List<User> users = _userManager.Users.Select(u => new Entity.Concrete.Identity.User
            {

                Id = u.Id,
                UserName = u.UserName
            }).ToList();


            List<Role> roles = _roleManager.Roles.Select(r => new Role
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();
            List<SelectListItem> selectRoleList = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            UserRolesDto userRolesDto = new UserRolesDto
            {
                SelectRoleList = selectRoleList,
                Users = users
            };
            return userRolesDto;
        }
        public IActionResult UserRoles()
        {
            var userRolesDto = GetUserRolesDto();
            ViewBag.SelectedMenu = "UserRoles";
            ViewBag.Title = "Rol Atama";
            return View(userRolesDto);
        }
        public async Task<IActionResult> GetUsers(UserRolesDto userRolesDto)
        {
            var role = await _roleManager.FindByIdAsync(userRolesDto.RoleId);
            var members = new List<User>();
            var nonMembers = new List<User>();
            var userRolesDtoInstance = GetUserRolesDto();
            foreach (var user in userRolesDtoInstance.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            var roleDetailsDto = new RoleDetailsDto
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            userRolesDto.SelectRoleList = userRolesDtoInstance.SelectRoleList;
            userRolesDto.RoleDetailsDto = roleDetailsDto;
            userRolesDto.Users = userRolesDtoInstance.Users;
            return View("UserRoles", userRolesDto);

        }
    }
}
