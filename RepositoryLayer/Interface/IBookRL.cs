using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IBookRL
    {
        public BookModel AddBook(BookModel book);
        public BookModel GetBookByBookId(int BookId);

        public List<BookModel> GetAllBooks();

    }
}
