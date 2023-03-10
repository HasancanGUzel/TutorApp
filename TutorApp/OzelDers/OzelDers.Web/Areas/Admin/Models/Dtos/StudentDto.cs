using Microsoft.AspNetCore.Mvc.Rendering;
using OzelDers.Entity.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OzelDers.Web.Areas.Admin.Models.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }


        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string FirstName { get; set; } //-------

        [DisplayName("SoyAd")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string LastName { get; set; }//-----------

        [DisplayName("Öğretmen Resmi")]
        [Required(ErrorMessage = "{0} seçilmelidir.")]
        public IFormFile ImageFile { get; set; }//--------------
        public string ImageUrl { get; set; }



        [DisplayName("İlçe")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Location { get; set; }//-------------

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string City { get; set; }//-------------


        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Adress { get; set; }//-------------
        [DisplayName("Cinsiyet")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Gender { get; set; }//-------------

        [DisplayName("Hakkımda")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string About { get; set; }//-------------

        [DisplayName("Doğum Tarihi")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]

        public DateTime DateOfBirth { get; set; }

        [DisplayName("İlan Ver")]

        public bool IsHome { get; set; }



        public List<SelectListItem> GenderSelectList { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> IlceList { get; set; }





        [DisplayName("Dersler")]
        public List<Lesson> Lessons { get; set; }

        [Required(ErrorMessage = "Bir Ders seçilmelidir.")]
        public int[] SelectedLessonIds { get; set; }





        //uSER İÇİN

        [DisplayName("kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string UserName { get; set; }

        [DisplayName("E-posta")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



    }
}
