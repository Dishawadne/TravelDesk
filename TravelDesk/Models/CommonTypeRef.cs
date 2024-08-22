namespace TravelDesk.Models
{
    public class CommonTypeRef
    {
        public int Id { get; set; }         // Unique identifier for the type (e.g., Role ID)
        public string Name { get; set; }     // Name of the type (e.g., "Manager", "Employee")
        public string Value { get; set; }
    }
}
