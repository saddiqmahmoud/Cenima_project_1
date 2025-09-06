using System.ComponentModel.DataAnnotations;

namespace Cenima_project.NewModels
{
    public class Movie
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MinLength(3, ErrorMessage = "max 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!")]
        public string? Description { get; set; }
        public double Price { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MinLength(3, ErrorMessage = "max 5")]
        public string ImgUrl { get; set; }//image
        public string TrailerUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieStatus MovieStatus { get; set; }

        public int CinemaId { get; set; }
        public int CategoryId { get; set; }
        public Cinema Cinema { get; set; }
        public Category Category { get; set; }

        public List<ActorMovie> ActorMovie { get; set; }
        
    }
}
