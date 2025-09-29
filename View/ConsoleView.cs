using System;

namespace LibrarySystemWithEF.Views
{
    public static class ConsoleView
    {

        // Pages
        public static (string Email, string Password) ShowLoginPage()
        {
            DrawHeader("Login Page");
            string email = ReadInput("Email: ");
            string password = ReadInput("Password: ");
            return (email, password);
        }

        public static (string FullName, string Email, string Password) ShowSignUpPage()
        {
            DrawHeader("Sign Up Page");
            string fullName = ReadInput("Full Name: ");
            string email = ReadInput("Email: ");
            string password = ReadInput("Password: ");
            return (fullName, email, password);
        }

        public static void ShowStartPage()
        {
            DrawHeader("Welcome to Library System");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
        }

        public static void ShowMainMenu()
        {
            DrawHeader("Library System - Main Menu");
            Console.WriteLine("1. List all Users");
            Console.WriteLine("2. List all Books");
            Console.WriteLine("3. Borrow a Book");
            Console.WriteLine("4. Return a Book");
            Console.WriteLine("5. Exit");
            Console.WriteLine("==========================");
            Console.Write("Choose an option: ");
        }


        // General Helpers
        private static void DrawHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine(title);
            Console.WriteLine("=====================================");
        }

        private static string ReadInput(string label, bool required = true)
        {
            string input;
            do
            {
                Console.Write(label);
                input = Console.ReadLine()?.Trim();
            } while (required && string.IsNullOrEmpty(input));

            return input;
        }

        // Message
        public static void ShowMessage(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
