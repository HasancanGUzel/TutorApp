using OzelDers.Entity.Concrete.Identity;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class RoleDetailsDto
    {
        public Role Role { get; set; }
        public List<User> Members { get; set; }
        public List<User> NonMembers { get; set; }
    }
}
