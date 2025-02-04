using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public class Member
    {
        // Properties
        public int MemberId { get; }
        public string Name { get;}
        public List<Book> BorrowedBooks { get;}
        public Member() { }

        // Constructor
        public Member(int memberId, string name)
        {
            MemberId = memberId;
            Name = name;
            BorrowedBooks = new List<Book>();
        }
       
        //// Method to borrow a book
        //public void BorrowBook(Book book)
        //{
        //    if (book.IsAvailable)
        //    {
        //        BorrowedBooks.Add(book);
        //        book.IsAvailable = false;
        //        Console.WriteLine($"{Name} borrowed \"{book.Title}\".");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Sorry, \"{book.Title}\" is already checked out.");
        //    }
        //}

        //// Method to return a book
        //public void ReturnBook(Book book)
        //{
        //    if (BorrowedBooks.Contains(book))
        //    {
        //        BorrowedBooks.Remove(book);
        //        book.IsAvailable = true;
        //        Console.WriteLine($"{Name} returned \"{book.Title}\".");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{Name} did not borrow \"{book.Title}\".");
        //    }
        //}

        // Override ToString() for display
        public override string ToString()
        {
            return $"Member: {Name} (ID: {MemberId})";
        }
    }
}
