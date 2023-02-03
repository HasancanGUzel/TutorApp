using OzelDers.Entity.Concrete.Identity;

namespace OzelDers.Web.Areas.Students.Models.Dtos
{
    public class CommentAddDto
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }

        public int TeacherId { get; set; }
    }


}
