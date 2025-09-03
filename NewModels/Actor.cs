using System.ComponentModel.DataAnnotations;

namespace Cenima_project.NewModels
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب يافندم!"),MaxLength(30,ErrorMessage ="max 30")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب يافندم!"), MaxLength(30, ErrorMessage = "max 30")]
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public string News { get; set; }

        public List<ActorMovie> ActorMovie { get; set; }
    }
}

