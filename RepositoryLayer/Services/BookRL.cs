using CommonLayer.model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        
        public BookModel AddBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("SPAddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure 
                };
                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@Rating", book.Rating);
                cmd.Parameters.AddWithValue("@PeopleRating", book.PeopleRating);
                cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@BookDetails", book.BookDetails);
                cmd.Parameters.AddWithValue("@BookQuantity", book.BookQuantity);
                cmd.Parameters.AddWithValue("@BookImage", book.BookImage);
                
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
