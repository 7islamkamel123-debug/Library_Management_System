using Library_Management_System;
using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;

class Programe
{
    static void Main(string[] args)
    {
        var _context = new LibraryContext();

        // ===========================
        // Insert Data (Authors, Publishers, Books, Readers)
        // ===========================
        var auther = new List<Auther>
        {
            new Auther { Name = "islam" },
            new Auther { Name = "kamel" },
            new Auther { Name = "Abdelna4" }
        };
        _context.Authers.AddRange(auther);
        _context.SaveChanges();

        var puplishers = new List<Publisher>
        {
            new Publisher { Name="gomaa"},
            new Publisher { Name="adam"},
            new Publisher { Name="zayed"}
        };
        _context.Publishers.AddRange(puplishers);
        _context.SaveChanges();

        var books = new List<Book> {
            new Book { Title = "C# Basics", Price = 150, AutherId = 1, PublisherId = 1 },
            new Book { Title = "ASP.NET Core", Price = 200, AutherId = 2, PublisherId = 2 },
            new Book { Title = "EF Core Guide", Price = 250, AutherId = 3, PublisherId = 3 }
        };
        _context.Books.AddRange(books);
        _context.SaveChanges();

        var readers = new List<Reader>
        {
            new Reader { Name="islam"},
            new Reader { Name="kamel"},
            new Reader { Name="Abdelnapi"}
        };
        _context.Readers.AddRange(readers);
        _context.SaveChanges();

        // Clear old relations before inserting new ones
        _context.ReaderBooks.RemoveRange(_context.ReaderBooks);
        _context.SaveChanges();

        var readerBooks = new List<ReaderBook>
        {
            new ReaderBook { BookId = 1, ReaderId = 1 },
            new ReaderBook { BookId = 2, ReaderId = 2 },
            new ReaderBook { BookId = 3, ReaderId = 3 }
        };

        foreach (var rb in readerBooks)
        {
            bool exists = _context.ReaderBooks
                .Any(x => x.BookId == rb.BookId && x.ReaderId == rb.ReaderId);

            if (!exists)
                _context.ReaderBooks.Add(rb);
        }
        _context.SaveChanges();

        // ===========================
        // Queries & Outputs
        // ===========================

        Console.OutputEncoding = System.Text.Encoding.UTF8; // << مهم

        Console.WriteLine("\n=============================");
        Console.WriteLine("\U0001F4D6 Books with Price > 150 (Ordered by Price)");
        Console.WriteLine("=============================");
        var bookDetails = _context.Books
            .Where(b => b.Price > 150)
            .OrderBy(b => b.Price)
            .Select(b => new { b.Title, b.Price })
            .ToList();
        foreach (var book in bookDetails)
            Console.WriteLine($"- {book.Title} | Price: {book.Price}");


        Console.WriteLine("\n=============================");
        Console.WriteLine("\U0001F3E2 Books with Author and Publisher");
        Console.WriteLine("=============================");
        var _include = _context.Books
           .Include(b => b.Auther)
           .Include(b => b.Publisher)
           .ToList();
        foreach (var Book in _include)
            Console.WriteLine($"- Book: {Book.Title} | Author: {Book.Auther.Name} | Publisher: {Book.Publisher.Name}");


        Console.WriteLine("\n=============================");
        Console.WriteLine("\U0001F465 Books with Readers");
        Console.WriteLine("=============================");
        var booksWithReaders = _context.Books
            .Include(b => b.ReaderBook)
            .ThenInclude(rb => rb.Reader)
            .ToList();
        foreach (var book in booksWithReaders)
        {
            Console.WriteLine($"\nBook: {book.Title}");
            foreach (var rb in book.ReaderBook)
                Console.WriteLine($"   - Reader: {rb.Reader.Name}");
        }


        Console.WriteLine("\n=============================");
        Console.WriteLine("\U0000270D Group Books by Author");
        Console.WriteLine("=============================");
        var BookAuther = _context.Books
            .Include(b => b.Auther)
            .GroupBy(m => m.Auther)
            .ToList();
        foreach (var autherr in BookAuther)
        {
            Console.WriteLine($"\nAuthor: {autherr.Key.Name}");
            foreach (var book in autherr)
                Console.WriteLine($"   - Book: {book.Title}");
        }
        Console.WriteLine("\n=============================");
        bool anyprice = _context.Books.Any(b => b.Price > 200);
        Console.WriteLine($"\n Any book price > 200? {anyprice}");
        Console.WriteLine("\n=============================");
        bool allExpensivePrice = _context.Books.All(b => b.Price > 100);
        Console.WriteLine($" All books price > 100? {allExpensivePrice}");

        Console.WriteLine("\n=============================");
        bool containsBook = _context.Books.Select(b => b.Title).Contains("ASP.NET Core");
        Console.WriteLine($" Contains 'ASP.NET Core'? {containsBook}");
        Console.WriteLine("\n=============================");
        // Lazy Loading (تأكد إن Navigation Properties virtual)
        var booksLazy = _context.Books.ToList();

        Console.WriteLine("\n LAZY LOADING:");
        foreach (var book in booksLazy)
        {
            Console.WriteLine($"Book: {book.Title}, Author: {book.Auther.Name}");
            foreach (var rb in book.ReaderBook)
            {
                Console.WriteLine($"    Reader: {rb.Reader.Name}");
            }
        }


    }
}
