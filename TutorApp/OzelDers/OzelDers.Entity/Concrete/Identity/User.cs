using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Entity.Concrete.Identity
{
    public class User:IdentityUser
    {
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
