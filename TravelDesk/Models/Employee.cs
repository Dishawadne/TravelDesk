using System.ComponentModel.DataAnnotations;
using TeavelDesk.Models;
using TravelDesk.Enum;


namespace TravelDesk.Models
{
    public class Employee
    {
        [Key] // Primary Key
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is required.")]
        [MaxLength(100, ErrorMessage = "Employee Name can't be longer than 100 characters.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        [MaxLength(100, ErrorMessage = "Project Name can't be longer than 100 characters.")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [MaxLength(100, ErrorMessage = "Department Name can't be longer than 100 characters.")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Reason for Traveling is required.")]
        [MaxLength(250, ErrorMessage = "Reason for Traveling can't be longer than 250 characters.")]
        public string ReasonForTraveling { get; set; }

        public Status Status { get; set; }

        // Navigation properties
        public virtual ICollection<AirBooking>? AirBookings { get; set; } = new HashSet<AirBooking>();
        public virtual ICollection<HotelBooking>? HotelBookings { get; set; } = new HashSet<HotelBooking>();
       // public ICollection<Document> Documents { get; set; }
    }

}

