using System.ComponentModel.DataAnnotations;

namespace LibraryProject.WebUI.Models
{
    public class GenreForm
    {
        [Required]
        public string Name { get; set; }
    }
}
