using Microsoft.EntityFrameworkCore;
using TravelDesk.Context;
using TravelDesk.IRepository;
using TravelDesk.Models;

namespace TravelDesk.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContexts _context;
        public UserRepository(DbContexts context)
        {
            _context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Users.Include(u => u.Role)
                .Include(u => u.Manager)
                .Where(u => u.RoleId != 1 && u.IsActive)  
                .ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Role)
                .Include(u => u.Manager)
                .FirstOrDefault(u => u.Id == id);
        }
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User UpdateUser(int id, User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Address = user.Address;
                existingUser.Department = user.Department;
                existingUser.MobileNum = user.MobileNum;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;
                _context.SaveChanges();
            }
            return existingUser;
        }
        public bool DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null && user.IsActive)
            {
                user.IsActive = false;
                user.ModifiedOn = DateTime.Now; 
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
