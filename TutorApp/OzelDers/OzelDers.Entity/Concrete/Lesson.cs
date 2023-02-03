using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzelDers.Entity.Concrete
{
    //öğretmenlerin cardında olucak dersler categoriler
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
          public List<TeacherLesson> TeacherLesson { get; set; } 
          public List<LessonBranch> LessonBranch { get; set; }
        public List<StudentLesson> StudentLesson { get; set; }



    }
}