using LibrarySystemWithEF.Models;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystemWithEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDBContext())
            {
                var firstNames = new List<string>
                {
                    "Ahmed","Mohamed","Mahmoud","Youssef","Mostafa","Ali","Omar","Ibrahim","Khaled","Tamer",
                    "Hassan","Hussein","Abdallah","Karim","Fahd","Adel","Sami","Magdy","Amr","Wael",
                    "Mona","Sara","Aya","Heba","Nour","Yasmin","Laila","Fatma","Salma","Dina"
                };

                var lastNames = new List<string>
                {
                    "Ali","Hassan","Ibrahim","Omar","Mahmoud","Mostafa","Youssef","Abdelrahman","Saad","Lotfy",
                    "Tawfik","Fathy","Nasser","Badawy","Hegazy","Shadad","Gamal","Said","Samir","Zaki"
                };

                // 1. Add 100 User
                if (!context.Users.Any())
                {
                    var users = new List<User>();
                    var rand = new Random();

                    for (int i = 1; i <= 100; i++)
                    {
                        string fName = firstNames[rand.Next(firstNames.Count)];
                        string lName = lastNames[rand.Next(lastNames.Count)];
                        string fullName = $"{fName} {lName}";

                        users.Add(new User
                        {
                            FullName = fullName,
                            Email = $"{fName.ToLower()}{i}@gmail.com",
                            Password = "A1234567"
                        });
                    }

                    context.Users.AddRange(users);
                    context.SaveChanges();
                    Console.WriteLine("100 Users added.");
                }

                // 2. Add 20 Books
                if (!context.Books.Any())
                {
                    var books = new List<Book>
                    {
                        new Book("Clean Code", "Robert C. Martin", 2008),
                        new Book("Design Patterns", "Erich Gamma", 1994),
                        new Book("Refactoring", "Martin Fowler", 1999),
                        new Book("The Pragmatic Programmer", "Andrew Hunt", 1999),
                        new Book("Domain-Driven Design", "Eric Evans", 2003),
                        new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002),
                        new Book("Working Effectively with Legacy Code", "Michael Feathers", 2004),
                        new Book("Head First Design Patterns", "Eric Freeman", 2004),
                        new Book("Entity Framework Core in Action", "Jon Smith", 2021),
                        new Book("ASP.NET Core in Action", "Andrew Lock", 2018),
                        new Book("Pro C# 9", "Andrew Troelsen", 2021),
                        new Book("Introduction to Algorithms", "Thomas H. Cormen", 2009),
                        new Book("Effective C#", "Bill Wagner", 2017),
                        new Book("CLR via C#", "Jeffrey Richter", 2012),
                        new Book("C# in Depth", "Jon Skeet", 2019),
                        new Book("Agile Principles, Patterns, and Practices", "Robert C. Martin", 2006),
                        new Book("Building Microservices", "Sam Newman", 2015),
                        new Book("Software Architecture Patterns", "Mark Richards", 2015),
                        new Book("Code Complete", "Steve McConnell", 2004),
                        new Book("Extreme Programming Explained", "Kent Beck", 2000)
                    };

                    context.Books.AddRange(books);
                    context.SaveChanges();
                    Console.WriteLine("20 Books added.");
                }

                // 3. Random Borrowings 
                if (!context.Borrowings.Any())
                {
                    var rand = new Random();
                    var users = context.Users.ToList();
                    var books = context.Books.ToList();

                    var borrowings = new List<Borrowing>();

                    for (int i = 0; i < 150; i++)
                    {
                        var user = users[rand.Next(users.Count)];
                        var book = books[rand.Next(books.Count)];

                        borrowings.Add(new Borrowing
                        {
                            UserId = user.UserId,
                            BookId = book.BookId,
                            BorrowDate = DateTime.Now.AddDays(-rand.Next(1, 60)), // 60 days period
                            ReturnDate = rand.Next(2) == 0 ? null : DateTime.Now.AddDays(-rand.Next(1, 30)),
                            IsReturned = rand.Next(2) == 1
                        });
                    }

                    context.Borrowings.AddRange(borrowings);
                    context.SaveChanges();
                    Console.WriteLine("Random borrowings added.");
                }

               
                Console.WriteLine($"\nUsers: {context.Users.Count()}");
                Console.WriteLine($"Books: {context.Books.Count()}");
                Console.WriteLine($"Borrowings: {context.Borrowings.Count()}");
            }
        }
    }
}
