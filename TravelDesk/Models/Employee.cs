namespace TravelDesk.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerEmail { get; set; }
        public ICollection<TravelRequest> TravelRequests { get; set; }
    }
}
