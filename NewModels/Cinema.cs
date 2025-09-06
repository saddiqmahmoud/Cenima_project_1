using System.ComponentModel.DataAnnotations;

namespace Cenima_project.NewModels
{
    public class Cinema
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MinLength(3, ErrorMessage = "max 30")]
        public string name { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MinLength(3, ErrorMessage = "max 30")]
        public string Description { get; set; }
        public string CinemaLogo { get; set; }
        public string Address { get; set; }
        public List<Movie> Movie { get; set; }

    }
}
