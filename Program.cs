using LibrarySystemWithEF.Models;
using LibrarySystemWithEF.Views;
using LibrarySystemWithEF.Services.Implementations;
using Project1;

namespace LibrarySystemWithEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new ApplicationDBContext();

            var userService = new UserService(context);
            var bookService = new BookService(context);
            var borrowingService = new BorrowingService(context);

            var menuController = new MenuController(userService, bookService, borrowingService);

            menuController.Run(); 
        }
    }
}
