using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);

        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(string category);

        Task<bool> BorrowBookAsync(int id);
        Task<bool> ReturnBookAsync(int id);

    }
}



//A method which return data , we use angle brackets in the task mentioning the retuntype
//but the method which performs only the actions and not return any data we don't specify return type
// eg update add and deletebook .