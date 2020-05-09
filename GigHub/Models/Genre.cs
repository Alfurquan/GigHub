using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}