using LibrarySystem.Functionality.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Functionality.Contexts
{
    public class FunctionalityContext : DbContext, IFunctionalityContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public FunctionalityContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(cs => new { cs.BookId, cs.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(cs => cs.Book)
                .WithMany(c => c.AuthorsName)
                .HasForeignKey(cs => cs.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(cs => cs.Author)
                .WithMany(s => s.BooksTitle)
                .HasForeignKey(cs => cs.AuthorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
