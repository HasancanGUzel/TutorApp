using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OzelDers.Entity.Concrete;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class BranchAddDto
    {
        [DisplayName("Branş Adı")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0}, {1} karakterden kısa olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0}, {1} karakterden uzun olmamalıdır.")]
        public string Name { get; set; }

        [DisplayName("Dersler")]
        public List<Lesson> Lessons { get; set; } // ürün ekleye tıklandığı zaman ekranın sağında kategorşleri lşstelemek için

        [Required(ErrorMessage = "En az bir ders seçilmelidir.")]
        public int[] SelectedLessonIds { get; set; }//seçili olan kategorilerin value yani id lerini burada tutyoruz

    }
}
