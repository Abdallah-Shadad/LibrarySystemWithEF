using System;
using System.Text.RegularExpressions;

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

            string fullName = ReadInput("Full Name");

            // Email validation
            string email;
            do
            {
                email = ReadInput("Email: ");
                if (!IsValidEmail(email))
                    ShowMessage("Invalid email format! Please try again.", ConsoleColor.Red);
            } while (!IsValidEmail(email));

            // Password validation
            string password;
            do
            {
                password = ReadInput("Password: ");
                if (!IsValidPassword(password))
                    ShowMessage("Password must be at least 6 characters, contain at least one letter and one number.", ConsoleColor.Red);
            } while (!IsValidPassword(password));

            return (fullName, email, password);
        }

        public static void ShowStartPage()
        {
            DrawHeader("Welcome to Library System");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }

        public static void ShowMainMenu()
        {
            DrawHeader("Library System - Main Menu");
            Console.WriteLine("1. List all Users");
            Console.WriteLine("2. List all Books");
            Console.WriteLine("3. Borrow a Book");
            Console.WriteLine("4. Return a Book");
            Console.WriteLine("6. Search for a Book");
            Console.WriteLine("7. View My Borrowed Books");
            Console.WriteLine("8. Settings");
            Console.WriteLine("0. Exit");
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

        public static string ReadInput(string label, bool required = true)
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

        // Validators
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                return false;
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$");
        }
    }
}
