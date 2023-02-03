using OzelDers.Entity.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class BranchUpdateDto
    {
        public int Id { get; set; }

        [DisplayName("Branş Adı")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0}, {1} karakterden kısa olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0}, {1} karakterden uzun olmamalıdır.")]
        public string Name { get; set; }

        public string Url { get; set; }

        [DisplayName("Dersler")]
        public List<Lesson> Lessons { get; set; } // ürün ekleye tıklandığı zaman ekranın sağında kategorşleri lşstelemek için

        [Required(ErrorMessage = "En az bir ders seçilmelidir.")]
        public int[] SelectedLessonIds { get; set; }
    }
}
