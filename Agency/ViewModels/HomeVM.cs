using Agency.Models;

namespace Agency.ViewModels
{
    public class HomeVM
    {
        public List<Models.Project> Projects { get; set; }
        public List<Models.Category> Categories { get; set; }
    }
}
