using LibrarySystemWithEF.Models;
using LibrarySystemWithEF.Services.Interfaces;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemWithEF.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDBContext _context;

        public BookService(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b=> b.BookId == id);    
        }

        public void MarkAsBorrowed(Book book)
        {
            book.IsAvailable = false;
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void MarkAsReturned(Book book)
        {
            book.IsAvailable = true;
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
