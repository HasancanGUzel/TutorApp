using Microsoft.AspNetCore.Mvc.Rendering;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class UserRolesDto
    {
        public List<User> Users { get; set; }
       
        public List<SelectListItem> SelectRoleList { get; set; }
        public string RoleId { get; set; }
        public RoleDetailsDto RoleDetailsDto { get; set; }
    }
}