using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OzelDers.Entity.Concrete;

namespace OzelDers.Web.Models.Dtos
{
    public class UserManageDto
    {
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



        //teacher ve student
        public string Id { get; set; }
        public string Tip { get; set; }

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

        [DisplayName("Ders Fiyatı")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public decimal? Price { get; set; }//-------------

        [DisplayName("İlçe")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Location { get; set; }//-------------

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string City { get; set; }//-------------

        [DisplayName("Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Deneyim")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Experience { get; set; }//-------------

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Adress { get; set; }//-------------
        [DisplayName("Cinsiyet")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string Gender { get; set; }//-------------

        [DisplayName("Hakkımda")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        public string About { get; set; }//-------------

        [DisplayName("İlan Ver")]
        public bool IsHome { get; set; }

        public List<SelectListItem> GenderSelectList { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> IlceList { get; set; }
        public List<SelectListItem> DeneyimList { get; set; }

        [DisplayName("Branşlar")]
        public List<Branch> Branches { get; set; }

        [Required(ErrorMessage = "Bir Branches seçilmelidir.")]
        public int SelectedBranchIds { get; set; }

        [DisplayName("Dersler")]
        public List<Lesson> Lessons { get; set; }

        [Required(ErrorMessage = "Bir Ders seçilmelidir.")]
        public int[] SelectedLessonIds { get; set; }


        //public Student Students { get; set; }

        //public Teacher Teachers { get; set; }


        //public class Teacher
        //{
        //    public string Id { get; set; }

        //    [DisplayName("Ad")]
        //    [Required(ErrorMessage = "{0} alanı zorunludur.")]
        //    public string FirstName { get; set; } //-------

        //    [DisplayName("SoyAd")]
        //    [Required(ErrorMessage = "{0} alanı zorunludur.")]
        //    public string LastName { get; set; }//-----------

        //    [DisplayName("Öğretmen Resmi")]
        //    [Required(ErrorMessage = "{0} seçilmelidir.")]
        //    public IFormFile ImageFile { get; set; }//--------------
        //    public string ImageUrl { get; set; }

        //    [DisplayName("Ders Fiyatı")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public decimal? Price { get; set; }//-------------

        //    [DisplayName("İlçe")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string Location { get; set; }//-------------

        //    [DisplayName("Şehir")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string City { get; set; }//-------------

        //    [DisplayName("Doğum Tarihi")]
        //    public DateTime DateOfBirth { get; set; }

        //    [DisplayName("Deneyim")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string Experience { get; set; }//-------------

        //    [DisplayName("Adres")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string Adress { get; set; }//-------------
        //    [DisplayName("Cinsiyet")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string Gender { get; set; }//-------------

        //    public List<SelectListItem> GenderSelectList { get; set; }
        //    public List<SelectListItem> CityList { get; set; }
        //    public List<SelectListItem> IlceList { get; set; }
        //    public List<SelectListItem> DeneyimList { get; set; }

        //    [DisplayName("Branşlar")]
        //    public List<Branch> Branches { get; set; }

        //    [Required(ErrorMessage = "Bir Branches seçilmelidir.")]
        //    public int SelectedBranchIds { get; set; }
        //}

        //public class Student
        //{
        //    //studetn için
        //    [DisplayName("Ad")]
        //    [Required(ErrorMessage = "{0} alanı zorunludur.")]
        //    public string FirstName { get; set; }

        //    [DisplayName("SoyAd")]
        //    [Required(ErrorMessage = "{0} alanı zorunludur.")]
        //    public string LastName { get; set; }

        //    [DisplayName("Cinsiyet")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string Gender { get; set; }

        //    [DisplayName("Şehir")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string City { get; set; }//-------------

        //    [DisplayName("İlçe")]
        //    [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        //    public string Location { get; set; }//-------------
        //    public List<SelectListItem> GenderSelectList { get; set; }
        //    public List<SelectListItem> CityList { get; set; }
        //    public List<SelectListItem> IlceList { get; set; }
        //}




    }
}
