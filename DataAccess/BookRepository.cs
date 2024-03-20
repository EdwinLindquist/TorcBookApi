using BookLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class BookRepository : IBookRepository
    {
        private readonly BookLibraryContext context;
        private DbSet<Book> dbSet;

        public BookRepository(BookLibraryContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Book>();
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public IEnumerable<Book> Get(
            Expression<Func<Book, bool>> filter = null,
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Book> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public Book GetBookByID(int id)
        {
            return context.Books.Find(id);
        }

        public void InsertBook(Book book)
        {
            context.Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            Book book = context.Books.Find(bookId);
            context.Books.Remove(book);
        }

        public void UpdateBook(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
