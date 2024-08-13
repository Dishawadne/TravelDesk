namespace TravelDesk.Models
{
    public class TravelRequest
    {
        public int TravelRequestId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string ReasonForTravel { get; set; }
        public string TypeOfBooking { get; set; }
        public DateTime DateOfRequest { get; set; }
        public ICollection<Document> Documents { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsApproved { get; set; }
        public string Status { get; set; }  
        public string Comments { get; set; }
    }
}
