using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }//bunu string yazmamızın nedeni role tablosunda id string olarak tutuluyor
        [DisplayName("Rol Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Name { get; set; }
        [DisplayName(" Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Description { get; set; }
    }
}
