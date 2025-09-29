using LibrarySystemWithEF.Models;
using LibrarySystemWithEF.Views;
using LibrarySystemWithEF.Services.Implementations;

namespace LibrarySystemWithEF
{
    public class MenuController
    {
        private readonly UserService _userService;
        private readonly BookService _bookService;
        private readonly BorrowingService _borrowingService;

        private User? _currentUser;

        private void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public MenuController(UserService userService, BookService bookService, BorrowingService borrowingService)
        {
            _userService = userService;
            _bookService = bookService;
            _borrowingService = borrowingService;
        }

        public void Run()
        {
            HandleLoginOrSignUp();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ConsoleView.ShowMainMenu();
                var choice = Console.ReadLine()?.Trim();

                Console.Clear();

                switch (choice)
                {
                    case "1": ListUsers(); Pause(); break;
                    case "2": ListBooks(); Pause(); break;
                    case "3": BorrowBook(); Pause(); break;
                    case "4": ReturnBook(); Pause(); break;
                    case "6": SearchBook(); Pause(); break;
                    case "7": ViewMyBorrowedBooks(); Pause(); break;
                    case "8": RunSettings(); break;
                    case "0": exit = true; break;
                    default:
                        ConsoleView.ShowMessage("Invalid choice. Try again.", ConsoleColor.Yellow);
                        Pause();
                        break;
                }
            }
        }

        private void HandleLoginOrSignUp()
        {
            while (_currentUser == null)
            {
                Console.Clear();
                ConsoleView.ShowStartPage();
                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": Login(); break;
                    case "2": SignUp(); break;
                    case "0": Environment.Exit(0); break;
                    default:
                        ConsoleView.ShowMessage("Invalid choice.", ConsoleColor.Yellow);
                        Pause();
                        break;
                }
            }

            ConsoleView.ShowMessage($"Welcome {_currentUser.FullName}!", ConsoleColor.Cyan);
            Pause();
        }

        private void Login()
        {
            var (email, password) = ConsoleView.ShowLoginPage();
            _currentUser = _userService.Login(email, password);

            if (_currentUser == null)
            {
                ConsoleView.ShowMessage("Invalid Email or Password.", ConsoleColor.Red);
                Pause();
            }
        }

        private void SignUp()
        {
            var (fullName, email, password) = ConsoleView.ShowSignUpPage();
            try
            {
                _currentUser = _userService.Register(fullName, email, password);
                ConsoleView.ShowMessage("Account created successfully!", ConsoleColor.Green);
                Pause();
            }
            catch (Exception ex)
            {
                ConsoleView.ShowMessage(ex.Message, ConsoleColor.Red);
                Pause();
            }
        }

        private void ListUsers()
        {
            var users = _userService.GetAllUsers();
            Console.WriteLine("======================================================");
            Console.WriteLine($"{"ID",-5} {"Full Name",-20} {"Email",-25}");
            Console.WriteLine("======================================================");

            foreach (var user in users)
                Console.WriteLine($"{user.UserId,-5} {user.FullName,-20} {user.Email,-25}");

            Console.WriteLine("======================================================");
        }

        private void ListBooks()
        {
            var books = _bookService.GetAllBooks();
            Console.WriteLine("=====================================================================================================");
            Console.WriteLine($"{"ID",-5} {"Title",-50} {"Author",-20} {"Year",-6} {"Available",-10}");
            Console.WriteLine("=====================================================================================================");

            foreach (var book in books)
            {
                string status = book.IsAvailable ? "Yes" : "No";
                string title = book.Title.Length > 50 ? book.Title.Substring(0, 47) + "..." : book.Title;

                Console.WriteLine($"{book.BookId,-5} {title,-50} {book.Author,-20} {book.Year,-6} {status,-10}");
            }

            Console.WriteLine("=====================================================================================================");
        }

        private void BorrowBook()
        {
            var availableBooks = _bookService.GetAllBooks().Where(b => b.IsAvailable).ToList();

            if (!availableBooks.Any())
            {
                ConsoleView.ShowMessage("No available books right now.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("Available Books:");
            foreach (var book in availableBooks)
                Console.WriteLine($"{book.BookId} - {book.Title}");

            Console.Write("Enter Book ID to borrow (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int bookId) || bookId < 0)
            {
                ConsoleView.ShowMessage("Invalid input. Please enter a valid Book ID.", ConsoleColor.Red);
                return;
            }

            if (bookId == 0) return;

            try
            {
                _borrowingService.BorrowBook(_currentUser.UserId, bookId);
                ConsoleView.ShowMessage("Book borrowed successfully!");
            }
            catch (Exception ex)
            {
                ConsoleView.ShowMessage(ex.Message, ConsoleColor.Red);
            }
        }

        private void ReturnBook()
        {
            var myBorrowings = _borrowingService.GetUserBorrowings(_currentUser.UserId);

            if (!myBorrowings.Any())
            {
                ConsoleView.ShowMessage("You have no borrowed books.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("Your Borrowed Books:");
            foreach (var b in myBorrowings)
            {
                var book = _bookService.GetBookById(b.BookId);
                Console.WriteLine($"{book.BookId} - {book.Title}");
            }

            Console.Write("Enter Book ID to return (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int returnBookId) || returnBookId < 0)
            {
                ConsoleView.ShowMessage("Invalid input. Please enter a valid Book ID.", ConsoleColor.Red);
                return;
            }

            if (returnBookId == 0) return;

            try
            {
                _borrowingService.ReturnBook(_currentUser.UserId, returnBookId);
                ConsoleView.ShowMessage("Book returned successfully!");
            }
            catch (Exception ex)
            {
                ConsoleView.ShowMessage(ex.Message, ConsoleColor.Red);
            }
        }

        private void SearchBook()
        {
            Console.Write("Enter book title or author to search: ");
            var keyword = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                ConsoleView.ShowMessage("Search keyword cannot be empty.", ConsoleColor.Yellow);
                return;
            }

            var results = _bookService.GetAllBooks()
                .Where(b => b.Title.ToLower().Contains(keyword) || b.Author.ToLower().Contains(keyword))
                .ToList();

            if (!results.Any())
            {
                ConsoleView.ShowMessage("No books found matching your search.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("Search Results:");
            foreach (var book in results)
                Console.WriteLine($"{book.BookId} - {book.Title} by {book.Author}");
        }

        private void ViewMyBorrowedBooks()
        {
            var myBorrowings = _borrowingService.GetUserBorrowings(_currentUser.UserId);

            if (!myBorrowings.Any())
            {
                ConsoleView.ShowMessage("You have not borrowed any books.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("My Borrowed Books:");
            foreach (var b in myBorrowings)
            {
                var book = _bookService.GetBookById(b.BookId);
                Console.WriteLine($"{book.BookId} - {book.Title} (Borrowed on {b.BorrowDate.ToShortDateString()})");
            }
        }

        // =========================
        // Settings
        // =========================
        private void RunSettings()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("===== Settings =====");
                Console.WriteLine("1. Change Full Name");
                Console.WriteLine("2. Change Email");
                Console.WriteLine("3. Change Password");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": ChangeFullName(); break;
                    case "2": ChangeEmail(); break;
                    case "3": ChangePassword(); break;
                    case "0": back = true; break;
                    default:
                        ConsoleView.ShowMessage("Invalid choice. Try again.", ConsoleColor.Yellow);
                        Pause();
                        break;
                }
            }
        }

       
        private bool ConfirmCurrentPassword()
        {
            string currentPassword = ConsoleView.ReadInput("Enter current password to confirm: ");
            if (_currentUser.Password != currentPassword)
            {
                ConsoleView.ShowMessage("Password is incorrect! Access denied.", ConsoleColor.Red);
                Pause();
                return false;
            }
            return true;
        }

        private void ChangeFullName()
        {
            if (!ConfirmCurrentPassword()) return;

            string newFullName = ConsoleView.ReadInput("Enter new full name: ");
            _currentUser.FullName = newFullName;
            _userService.UpdateUser(_currentUser);
            ConsoleView.ShowMessage("Full name updated successfully!");
            Pause();
        }

        private void ChangeEmail()
        {
            if (!ConfirmCurrentPassword()) return;

            string newEmail;
            do
            {
                newEmail = ConsoleView.ReadInput("Enter new email: ");
                if (!ConsoleView.IsValidEmail(newEmail))
                {
                    ConsoleView.ShowMessage("Invalid email format! Please try again.", ConsoleColor.Red);
                    continue;
                }
                if (_userService.GetAllUsers().Any(u => u.Email == newEmail && u.UserId != _currentUser.UserId))
                {
                    ConsoleView.ShowMessage("Email already exists!", ConsoleColor.Red);
                    continue;
                }
                break;
            } while (true);

            _currentUser.Email = newEmail;
            _userService.UpdateUser(_currentUser);
            ConsoleView.ShowMessage("Email updated successfully!");
            Pause();
        }

        private void ChangePassword()
        {
            if (!ConfirmCurrentPassword()) return;

            string newPassword;
            do
            {
                newPassword = ConsoleView.ReadInput("Enter new password: ");
                if (!ConsoleView.IsValidPassword(newPassword))
                {
                    ConsoleView.ShowMessage("Password must be at least 6 characters, contain at least one letter and one number.", ConsoleColor.Red);
                    continue;
                }
                break;
            } while (true);

            _currentUser.Password = newPassword;
            _userService.UpdateUser(_currentUser);
            ConsoleView.ShowMessage("Password updated successfully!");
            Pause();
        }


    }
}
