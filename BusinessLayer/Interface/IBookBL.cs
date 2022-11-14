using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);
        public BookModel GetBookByBookId(int BookId);
        public List<BookModel> GetAllBooks();

        public bool DeleteBook(int BookId);
    }
}
