using OzelDers.Entity.Concrete;

namespace OzelDers.Web.Areas.Students.Models.Dtos
{
    public class TeacherDetailsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Price { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Experience { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }

        public Branch Branch { get; set; }
        public List<Lesson> Lessons { get; set; }

        public List<Comment> Comments { get; set; }
        public CommentAddDto CommentAddDto { get; set; }
    }
}
