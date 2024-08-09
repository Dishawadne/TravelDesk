using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TravelDesk.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(10, ErrorMessage = "Mobile number cannot be longer than 10 characters.")]
        public string MobileNum { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required]
        
        public string Department { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

       
        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public User? Manager { get; set; }

        // Audit fields
        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

       
    }
}
