namespace TravelDesk.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DocumentType { get; set; }
        public int TravelRequestId { get; set; }
        public TravelRequest TravelRequest { get; set; }
    }
}
