using OzelDers.Entity.Concrete;

namespace OzelDers.Web.Areas.Teachers.Models.Dtos
{
    public class StudentDetailsDto
    {
        public int Id { get; set; }
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
        public List<Lesson> Lessons { get; set; }
    }
}
