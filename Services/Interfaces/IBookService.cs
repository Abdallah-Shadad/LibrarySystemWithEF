using LibrarySystemWithEF.Models;
using System.Collections.Generic;

namespace LibrarySystemWithEF.Services.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        void AddBook(Book book);
        void MarkAsBorrowed(Book book);
        void MarkAsReturned(Book book);
    }
}
