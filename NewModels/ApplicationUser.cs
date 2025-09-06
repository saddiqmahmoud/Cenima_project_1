using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cenima_project.NewModels
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string?  Address { get; set; }
    }
}
