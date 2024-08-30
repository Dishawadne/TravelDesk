using TravelDesk.Context;

using TravelDesk.IRepository;
using TravelDesk.Models;

namespace TravelDesk.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbContexts _context;

        public AuthRepository(DbContexts context)
        {
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

            return user; 
        }
    }
}
