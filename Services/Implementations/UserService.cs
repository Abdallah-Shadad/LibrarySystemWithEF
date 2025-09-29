using LibrarySystemWithEF.Models;
using LibrarySystemWithEF.Services.Interfaces;
using Project1;
using System.Linq;

namespace LibrarySystemWithEF.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public User? Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User? Register(string fullName, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                throw new Exception("Email already exists!");
            }

            var user = new User(fullName, email, password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
