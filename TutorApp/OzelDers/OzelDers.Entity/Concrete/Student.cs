using OzelDers.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Entity.Concrete
{
    public class Student:BaseUserEntity
    {

        public string UserId { get; set; }
        public User User { get; set; }
        public List<StudentLesson> StudentLesson { get; set; }

    }
}
