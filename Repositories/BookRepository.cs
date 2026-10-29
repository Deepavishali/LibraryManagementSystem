using LibraryManagement.Models;
using LibraryManagement.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories
{
    public class BookRepository:IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

      public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.books.FindAsync(id);
            if(book != null)
            {
                _context.books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _context.books.Where(b => b.BookName.Contains(searchTerm) || b.Author.Contains(searchTerm)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(string category)
        {
            return await _context.books.Where(b => b.Category.ToLower() == category.ToLower()).ToListAsync();
        }

        public async Task<bool> BorrowBookAsync(int id)
        {
            var book = await _context.books.FindAsync(id);
            if(book == null || book.AvailableCopies <= 0)
            {
                return false;
            }
            book.AvailableCopies -= 1;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int id)
        {
            var book = await _context.books.FindAsync(id);
            if(book == null || book.AvailableCopies >= book.Quantity)
            {
                return false;
            }
            book.AvailableCopies += 1;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
