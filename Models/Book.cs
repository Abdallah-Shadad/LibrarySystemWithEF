using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemWithEF.Models
{
    public class Book
    {

        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Borrowing> Borrowings { get; set; }


        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString()
        {
            return $"Book: {Title} by {Author}, Year: {Year}, Available: {IsAvailable}";
        }
    }
}
