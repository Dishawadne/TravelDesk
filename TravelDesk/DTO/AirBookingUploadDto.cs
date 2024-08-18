using System.ComponentModel.DataAnnotations;
using TravelDesk.Enum;
using TravelDesk.Models;


namespace TravelDesk.DTO
{
    public class AirBookingUploadDto
    {

        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Flight Type is required.")]
        public FlightType FlightType { get; set; }

        public string? AadharCardNumber { get; set; }

        // These properties can be kept if needed to capture the input data.
        public string? PassportNumber { get; set; }
        // File uploads for passport and visa
        public IFormFile? PassportFile { get; set; }
        public IFormFile? VisaFile { get; set; }
    }
}