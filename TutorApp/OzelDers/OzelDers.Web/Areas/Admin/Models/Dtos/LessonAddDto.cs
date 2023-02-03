using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class LessonAddDto
    {
        [DisplayName("Ders Adı")]
        [Required(ErrorMessage = "{0} boş bırkaılmamalıdır")]
        [MinLength(5, ErrorMessage = "{0} , {1} karakterden kısa olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0}, {1} karakterden uzun olmamalıdır")]
        public string Name { get; set; }
    }
}
