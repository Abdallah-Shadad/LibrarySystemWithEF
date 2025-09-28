using LibrarySystemWithEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibrarySystemWithEF.Views
{
    public static class ConsoleView
    {
        public static void ShowMainMenu()
        {
            Console.WriteLine("===== Library System =====");
            Console.WriteLine("1. List all Users");
            Console.WriteLine("2. List all Books");
            Console.WriteLine("3. Borrow a Book");
            Console.WriteLine("4. Return a Book");
            Console.WriteLine("5. Exit");
            Console.WriteLine("==========================");
            Console.Write("Choose an option: ");
        }

        public static void ShowMessage(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
