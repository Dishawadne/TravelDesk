namespace TravelDesk.Models
{
    public enum Department
    {
        HR,
        IT,
        Finance,
        Sales
    }
    public enum TravelRequestStatus
    {
        Pending,//0
        Approved,//1
        Rejected,//2
        Booked,//3
        ReturnedToManager,//4
        ReturnedToEmployee,//5
        Completed//6
    }
}
