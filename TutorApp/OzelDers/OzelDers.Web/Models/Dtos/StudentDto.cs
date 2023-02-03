using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using OzelDers.Entity.Concrete;

namespace OzelDers.Web.Models.Dtos
{
    public class StudentDto
    {
        //studetn için
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string FirstName { get; set; }

        [DisplayName("SoyAd")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string LastName { get; set; }

        [DisplayName("Cinsiyet")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Gender { get; set; }

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string City { get; set; }//-------------

        [DisplayName("İlçe")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Location { get; set; }//-------------


        [DisplayName("Hakkımda")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string About { get; set; }//-------------


        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Adress { get; set; }//-------------

        [DisplayName("Öğrenci Resmi")]
        [Required(ErrorMessage = "{0} seçilmelidir.")]
        public IFormFile ImageFile { get; set; }//--------------
        public string ImageUrl { get; set; }

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


        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        [DisplayName("Parola")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Parola")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parola ile {0} aynı olmalıdır")]
        public string RePassword { get; set; }
    }
}
