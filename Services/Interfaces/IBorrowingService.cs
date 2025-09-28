using LibrarySystemWithEF.Models;
using System.Collections.Generic;

namespace LibrarySystemWithEF.Services.Interfaces
{
    public interface IBorrowingService
    {
        List<Borrowing> GetAllBorrowings();
        void BorrowBook(int userId, int bookId);
        void ReturnBook(int borrowingId);
    }
}
