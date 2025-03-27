using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace E_LearningApp_WEBAPI.Models
{
    public class ChangePasswordDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? CurrentPassword { get; set; }
        [Required]
        public string? NewPassword { get; set; }
        [Required]
        public string? ConfirmNewPassword { get; set; }
    }
}
