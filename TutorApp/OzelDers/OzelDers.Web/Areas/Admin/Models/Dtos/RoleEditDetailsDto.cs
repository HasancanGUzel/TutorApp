namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class RoleEditDetailsDto
    {
        public string[] IdsToRemove { get; set; }
        public string[] IdsToAdd { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
