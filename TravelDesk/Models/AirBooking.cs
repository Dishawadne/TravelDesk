using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TravelDesk.Enum;
using System.Text.Json.Serialization;
using TravelDesk.Models;

namespace TeavelDesk.Models
{
    public class AirBooking
    {
        [Key] // Primary Key
        public int AirBookingId { get; set; }

        [Required(ErrorMessage = "Booking Date is required.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Flight Type is required.")]
        public FlightType FlightType { get; set; }

        public string? AadharCardNumber { get; set; }
        public string? PassportNumber { get; set; }


        public string? PassportFileName { get; set; } // Store file name
        public string? VisaFileName { get; set; } // Store file name



        [NotMapped] // Assuming file upload will be handled separately
        public IFormFile? PassportFile { get; set; }

        [NotMapped] // Assuming file upload will be handled separately
        public IFormFile? VisaFile { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        [JsonIgnore] // Avoid circular reference
        public virtual Employee Employee { get; set; }

    }
}
