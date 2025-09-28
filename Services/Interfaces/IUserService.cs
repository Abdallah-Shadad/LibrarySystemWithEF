using LibrarySystemWithEF.Models;
using System.Collections.Generic;

namespace LibrarySystemWithEF.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User? GetUserById(int id);
        void AddUser(User user);
    }
}
