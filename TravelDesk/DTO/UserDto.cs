using TravelDesk.Models;

namespace TravelDesk.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public ICollection<TravelRequest> TravelRequests { get; set; }
    }
}
