using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LibraryManagementSystem
{
    public class Library
    {
        private const string BooksFile = "books.json";
        private const string MembersFile = "members.json";

        public List<Book> Books { get; private set; }
        public List<Member> Members { get; private set; }

        public Library()
        {
            Books = new List<Book>();
            Members = new List<Member>();
            LoadData();
        }
        // 📌 Borrow a Book
        public bool BorrowBook(int bookId, int memberId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            var member = Members.FirstOrDefault(m => m.MemberId == memberId);

            if (book == null)
            {
                Console.WriteLine("❌ Book not found.");
                return false;
            }

            if (member == null)
            {
                Console.WriteLine("❌ Member not found.");
                return false;
            }

            if (book.Borrow(memberId))
            {
                SaveData();
                Console.WriteLine($"✅ '{book.Title}' has been borrowed by {member.Name}.");
                return true;
            }

            Console.WriteLine("❌ Book is already borrowed.");
            return false;
        }

        // 📌 Return a Book
        public bool ReturnBook(int bookId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                Console.WriteLine("❌ Book not found.");
                return false;
            }

            if (book.Return())
            {
                SaveData();
                Console.WriteLine($"✅ '{book.Title}' has been returned successfully.");
                return true;
            }

            Console.WriteLine("❌ Book was not borrowed.");
            return false;
        }


        // Search books by Title, Author, or Availability
        public List<Book> SearchBooks(string keyword, bool? available = null)
        {
            return Books
                .Where(b => b != null &&  //  Prevent NullReferenceException
                            !string.IsNullOrWhiteSpace(b.Title) &&
                            !string.IsNullOrWhiteSpace(b.Author) &&
                            (b.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                             b.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                .Where(b => available == null || b.IsAvailable == available)
                .ToList();
        }

        // Search members by Name or ID
        public List<Member> SearchMembers(string keyword)
        {
            return Members
                .Where(m => m != null &&  //Prevent NullReferenceException
                            !string.IsNullOrWhiteSpace(m.Name) &&
                            (m.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                             m.MemberId.ToString().Contains(keyword)))
                .ToList();
        }

        // Add Book
        public void AddBook(Book book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
            {
                Console.WriteLine("Invalid book! Title and Author cannot be empty.");
                return;
            }

            if (Books.Any(b => b.Id == book.Id))
            {
                Console.WriteLine($"Book with ID {book.Id} already exists!");
                return;
            }

            Books.Add(book);
            SaveData();
            Console.WriteLine($" Book '{book.Title}' added successfully!");
        }

        //  Add Member
        public void AddMember(Member member)
        {
            if (member == null || string.IsNullOrWhiteSpace(member.Name))
            {
                Console.WriteLine("Invalid member! Name cannot be empty.");
                return;
            }

            if (Members.Any(m => m.MemberId == member.MemberId))
            {
                Console.WriteLine($" Member with ID {member.MemberId} already exists!");
                return;
            }

            Members.Add(member);
            SaveData();
            Console.WriteLine($"Member '{member.Name}' added successfully!");
        }

        //  List all books
        public void ListAllBooks()
        {
            Console.WriteLine("\n All Books:");
            if (Books.Count == 0) Console.WriteLine("No books available.");
            else Books.ForEach(b => Console.WriteLine(b));
        }

        //  List all members
        public void ListAllMembers()
        {
            Console.WriteLine("\nAll Members:");
            if (Members.Count == 0) Console.WriteLine("No members found.");
            else Members.ForEach(m => Console.WriteLine(m));
        }

        private void SaveData()
        {
            try
            {
                File.WriteAllText(BooksFile, JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true }));
                File.WriteAllText(MembersFile, JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                if (!File.Exists(BooksFile)) File.WriteAllText(BooksFile, "[]");
                if (!File.Exists(MembersFile)) File.WriteAllText(MembersFile, "[]");

                string booksJson = File.ReadAllText(BooksFile);
                string membersJson = File.ReadAllText(MembersFile);

                Books = JsonSerializer.Deserialize<List<Book>>(booksJson) ?? new List<Book>();
                Members = JsonSerializer.Deserialize<List<Member>>(membersJson) ?? new List<Member>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                Books = new List<Book>();  // Reset to empty list if JSON is corrupted
                Members = new List<Member>();
            }
        }
    }
}
