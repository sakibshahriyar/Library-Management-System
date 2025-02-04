using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    class Program
    {
        static Library library = new Library();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n===== Library Management System =====");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Add Member");
                Console.WriteLine("3. Search Books");
                Console.WriteLine("4. Search Members");
                Console.WriteLine("5. Borrow Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("7. List All Books");
                Console.WriteLine("8. List All Members");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": AddMember(); break;
                    case "3": SearchBooks(); break;
                    case "4": SearchMembers(); break;
                    case "5": BorrowBook(); break;
                    case "6": ReturnBook(); break;
                    case "7": library.ListAllBooks(); break;
                    case "8": library.ListAllMembers(); break;
                    case "9":
                        Console.WriteLine("Exiting... Goodbye!");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid choice! Try again.");
                        break;
                }
            }
        }
        static void AddBook()
        {
            try
            {
                Console.Write("\nEnter Book ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Enter Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter Author: ");
                string author = Console.ReadLine();

                library.AddBook(new Book (id, title, author, true));  // constructor
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void AddMember()
        {
            try
            {
                Console.Write("\nEnter Member ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                library.AddMember(new Member(id, name));  //  Pass required parameters
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static void BorrowBook()
        {
            try
            {
                Console.Write("\nEnter Book ID to Borrow: ");
                int bookId = int.Parse(Console.ReadLine());
                Console.Write("Enter Member ID: ");
                int memberId = int.Parse(Console.ReadLine());

                library.BorrowBook(bookId, memberId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        static void ReturnBook()
        {
            try
            {
                Console.Write("\nEnter Book ID to Return: ");
                int bookId = int.Parse(Console.ReadLine());

                library.ReturnBook(bookId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
    

        static void SearchBooks()
        {
            Console.Write("\nEnter keyword (title/author): ");
            string keyword = Console.ReadLine();
            Console.Write("Filter by availability? (yes/no): ");
            string availabilityInput = Console.ReadLine().ToLower();
            bool? available = (availabilityInput == "yes") ? true :
                              (availabilityInput == "no") ? false : (bool?)null;

            var results = library.SearchBooks(keyword, available);
            Console.WriteLine("\n=====Search Results: Books =====");
            if (results.Count == 0)
                Console.WriteLine("No books found.");
            else
                results.ForEach(b => Console.WriteLine(b));
        }

        static void SearchMembers()
        {
            Console.Write("\nEnter keyword (name/ID): ");
            string keyword = Console.ReadLine();

            var results = library.SearchMembers(keyword);
            Console.WriteLine("\n===== Search Results: Members =====");
            if (results.Count == 0)
                Console.WriteLine("No members found.");
            else
                results.ForEach(m => Console.WriteLine(m));
        }
    }
}
