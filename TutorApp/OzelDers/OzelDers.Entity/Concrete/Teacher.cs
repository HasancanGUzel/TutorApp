using OzelDers.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Entity.Concrete
{
    public class Teacher:BaseUserEntity
    {

        public string UserId { get; set; }
        public User User { get; set; }
        public decimal? Price { get; set; }
        
       
        public string Experience { get; set; }
        public List<TeacherLesson> TeacherLesson { get; set; }
       
        public int BranchId { get; set; }//1 kullanıcı yani öğretmen için branş bilgisini tutucaz
        public Branch Branch { get; set; }
    }
}
