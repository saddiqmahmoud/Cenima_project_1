namespace Cenima_project.NewModels
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movie { get; set; }

    }
}
