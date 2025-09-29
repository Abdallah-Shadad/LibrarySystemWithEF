using LibrarySystemWithEF.Models;
using LibrarySystemWithEF.Services.Interfaces;
using Project1;

public class BorrowingService : IBorrowingService
{
    private readonly ApplicationDBContext _context;

    public BorrowingService(ApplicationDBContext context)
    {
        _context = context;
    }

    public void BorrowBook(int userId, int bookId)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

        if (user == null || book == null)
            throw new Exception("User or Book not found.");

        if (!book.IsAvailable)
            throw new Exception("Book is already borrowed.");

        var borrowing = new Borrowing
        {
            UserId = userId,
            BookId = bookId,
            BorrowDate = DateTime.Now,
            IsReturned = false
        };

        book.IsAvailable = false;
        _context.Borrowings.Add(borrowing);
        _context.SaveChanges();
    }

    public List<Borrowing> GetAllBorrowings()
    {
        throw new NotImplementedException();
    }

    public List<Borrowing> GetUserBorrowings(int userId)
    {
        return _context.Borrowings
            .Where(b => b.UserId == userId && !b.IsReturned)
            .ToList();
    }

    public void ReturnBook(int userId, int bookId)
    {
        var borrowing = _context.Borrowings
            .FirstOrDefault(b => b.UserId == userId && b.BookId == bookId && !b.IsReturned);

        if (borrowing == null)
            throw new Exception("You have not borrowed this book.");

        borrowing.IsReturned = true;
        borrowing.ReturnDate = DateTime.Now;

        var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
        if (book != null)
        {
            book.IsAvailable = true;
        }

        _context.SaveChanges();
    }

}
