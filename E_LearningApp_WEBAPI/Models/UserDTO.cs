using System.ComponentModel.DataAnnotations;

namespace E_LearningApp_WEBAPI.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        public int? RoleId { get; set; }

        //public int? RoleId { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime? LastLogin { get; set; }
    }
}
