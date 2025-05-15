using Agency.Models;

namespace Agency.ViewModels
{
    public class CreateProjectVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        public IFormFile Photo { get; set; }
    }
}
