using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace TravelDesk.Models
{
    public class TravelRequest
    {
        [Key]
        public int RequestId { get; set; }

        
        public int UserId { get; set; }
        public User? User { get; set; } // Auto-fill with User info (Name, Department)

        public string ReasonForTravelling { get; set; }
        public int? ManagerId {  get; set; }
        public User? Manager { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public TravelRequestStatus Status { get; set; } = TravelRequestStatus.Pending; // Default status is "Pending"

        public string AddharCard { get; set; } // Assume this will store the path to the file


       public string? Comments { get; set; }
        // Audit fields
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set;}
        
    }
}
