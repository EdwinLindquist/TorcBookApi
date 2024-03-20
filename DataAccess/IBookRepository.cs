using BookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IBookRepository : IDisposable
    {
        IEnumerable<Book> Get(Expression<Func<Book, bool>> filter = null, Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null, string includeProperties = "");
        IEnumerable<Book> GetBooks();
        Book GetBookByID(int bookId);
        void InsertBook(Book book);
        void DeleteBook(int bookID);
        void UpdateBook(Book book);
        void Save();
    }
}
