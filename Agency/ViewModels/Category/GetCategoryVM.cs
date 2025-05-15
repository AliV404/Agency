namespace Agency.ViewModels
{
    public class GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Models.Project> Projects { get; set; }
    }
}
