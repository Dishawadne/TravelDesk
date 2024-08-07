using TravelDesk.Models;

namespace TravelDesk.IRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User AddUser(User user);
        User UpdateUser(int id, User user);
        bool DeleteUser(int id);
    }
}
