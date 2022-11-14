using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        
        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
