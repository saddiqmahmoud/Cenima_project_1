using System.ComponentModel.DataAnnotations;

namespace Cenima_project.NewModels
{
    public class Category
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MinLength(3, ErrorMessage = "max 3")]
        public string Name { get; set; }
        public List<Movie> Movie { get; set; }

    }
}
