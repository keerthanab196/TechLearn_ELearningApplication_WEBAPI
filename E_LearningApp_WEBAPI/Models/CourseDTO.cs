using System.ComponentModel.DataAnnotations;

namespace E_LearningApp_WEBAPI.Models
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        
    }
}
