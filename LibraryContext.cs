using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class LibraryContext:DbContext
    {
      
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=(LOCALDB)\MSSQLLocalDB;Database=libraryMangement;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relation betwean (book and reader ) many to many
            modelBuilder.Entity<ReaderBook>()
                .HasKey(x =>new { x.BookId , x.ReaderId});

            modelBuilder.Entity<ReaderBook>()
                .HasOne(rb => rb.Book)
                  .WithMany(b => b.ReaderBook)
                     .HasForeignKey(rb => rb.BookId);

            modelBuilder.Entity<ReaderBook>()
                .HasOne(rb => rb.Reader)
                  .WithMany(b => b.ReaderBooks)
                    .HasForeignKey(rb => rb.ReaderId);

            // relation betwean (book and publisher ) one to many
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                 .WithMany(b => b.Books)
                 .HasForeignKey(b=>b.PublisherId);

            // relation betwean (book and auther ) one to many

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Auther)
                   .WithMany(b => b.Books)
                      .HasForeignKey(b => b.AutherId);

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Auther> Authers { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ReaderBook> ReaderBooks { get; set; }  // مهم للجدول الوسيط

    }
}
