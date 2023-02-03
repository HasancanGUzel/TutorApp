using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzelDers.Entity.Concrete
{
    //öğretmenin branşı için 
    //bunu user ile birleştiricem
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<LessonBranch> LessonBranch { get; set; } 
        public List<Teacher> Teachers { get; set; }
    }
}