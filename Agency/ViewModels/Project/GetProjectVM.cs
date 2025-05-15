using Agency.Models;

namespace Agency.ViewModels
{
    public class GetProjectVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
