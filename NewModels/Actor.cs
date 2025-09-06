using System.ComponentModel.DataAnnotations;

namespace Cenima_project.NewModels
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب يافندم!"),MinLength(30,ErrorMessage ="max 3")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MinLength(30, ErrorMessage = "max 3")]
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public string News { get; set; }

        public List<ActorMovie> ActorMovie { get; set; }
    }
}

