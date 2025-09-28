using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace LibrarySystemWithEF.Models
{

    public class Borrowing
    {
        [Key]
        public int BorrowingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;

        public override string ToString()
        {
            string status = IsReturned ? $"Returned on {ReturnDate?.ToShortDateString()}" : "Not Returned";
            return $"Borrowing: {User?.FullName} borrowed {Book?.Title} on {BorrowDate.ToShortDateString()} - {status}";
        }
    }

}
