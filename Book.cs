using System;

namespace LibraryManagementSystem
{
    public class Book
    {
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public bool IsAvailable { get; private set; }
        public int? BorrowerId { get; private set; }  // Nullable: stores Member ID if borrowed

        // Constructor
        public Book(int id, string title, string author, bool isAvailable = true)
        {
            Id = id;
            Title = title;
            Author = author;
            IsAvailable = isAvailable;
            BorrowerId = null;  // Initially, no borrower
        }

        // Borrow Book
        public bool Borrow(int memberId)
        {
            if (!IsAvailable) return false;  // Already borrowed
            IsAvailable = false;
            BorrowerId = memberId;
            return true;
        }

        // Return Book
        public bool Return()
        {
            if (IsAvailable) return false;  // Already returned
            IsAvailable = true;
            BorrowerId = null;
            return true;
        }

        public override string ToString()
        {
            return $"{Id} - {Title} by {Author} - {(IsAvailable ? "Available" : $"Borrowed by Member {BorrowerId}")}";
        }
    }
}
