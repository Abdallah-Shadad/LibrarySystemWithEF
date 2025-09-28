using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemWithEF.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; } = "1234";

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

        public User() { }
        public User(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public User(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            return $"User: {FullName} ({Email})";
        }
    }

}
