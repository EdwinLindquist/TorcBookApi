using DataAccess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository bookRepository;

        public BookController()
        {
            this.bookRepository = new BookRepository(new BookLibraryContext());
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            var books = from b in bookRepository.GetBooks()
                           select b;
            return books;
        }

        [HttpGet("{id}")]
        public IEnumerable<Book> Get(int id)
        {
            var books = from b in bookRepository.Get( o => o.BookId == id)
                        select b;
            return books;
        }

        [HttpGet("{field}/{value}")]
        public IEnumerable<Book> Get(string field, string value)
        {
            IEnumerable<Book> books = null;

            if (field.ToLower() == "isbn")
            {
                books = from b in bookRepository.Get(o => o.Isbn == value)
                        select b;
            }

            if(field.ToLower() == "author")
            {
                books = from b in bookRepository.Get(o => (o.FirstName + " " + o.LastName).ToLower().Contains(value))
                        select b;
            }

            return books;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
