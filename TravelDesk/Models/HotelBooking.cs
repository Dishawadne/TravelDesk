using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TravelDesk.Enum;


namespace TravelDesk.Models
{
    public class HotelBooking
    {
        [Key] // Primary Key
        public int HotelBookingId { get; set; }

        [Required(ErrorMessage = "Booking Date is required.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Days of Stay is required.")]
        public int DaysOfStay { get; set; }

        public  MealRequired mealRequired{ get; set; }
        public MealPreference mealPreference { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        [JsonIgnore] // Avoid circular reference
        public virtual Employee Employee { get; set; }
    }

}

