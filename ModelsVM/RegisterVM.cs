using System.ComponentModel.DataAnnotations;

namespace Cenima_project.ModelsVM
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, DataType(DataType.Password),Compare("Password")]
        public string ConformPassword { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
