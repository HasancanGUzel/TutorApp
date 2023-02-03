using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OzelDers.Web.Models.Dtos
{
    public class LoginDto
    {
        [DisplayName("Email Adresi")]
        [Required(ErrorMessage = "{0} boş bırakılmamalı.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} boş bırakılmamalı.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
