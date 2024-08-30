namespace TravelDesk.DTO
{
    public class TravelRequestHistoryDto
    {
        public int RequestId { get; set; }
        public string ProjectName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string ReasonForTravelling { get; set; }
        public string Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
    }
}
