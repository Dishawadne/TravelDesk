using TravelDesk.Models;

namespace TravelDesk.IRepository
{
    public interface IAuthRepository
    {
        User Authenticate(string email, string password);
    }
}
