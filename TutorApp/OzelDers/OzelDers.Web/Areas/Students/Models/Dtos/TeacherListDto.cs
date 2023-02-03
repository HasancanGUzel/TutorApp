using OzelDers.Entity.Concrete;

namespace OzelDers.Web.Areas.Students.Models.Dtos
{
    public class TeacherListDto
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

        public List<Branch> Branches { get; set; }

    }
}
