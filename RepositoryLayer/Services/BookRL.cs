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

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spGetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel book = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                book.BookName = reader["BookName"].ToString();
                book.AuthorName = reader["AuthorName"].ToString();
                book.Rating = Convert.ToInt32(reader["Rating"]);
                book.PeopleRating = Convert.ToInt32(reader["PeopleRating"]);
                book.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                book.BookDetails = reader["BookDetails"].ToString();
                book.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                book.BookImage = reader["BookImage"].ToString();
                

                return book;
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

        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spGetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["BookName"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            PeopleRating = Convert.ToInt32(reader["PeopleRating"]),
                            DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]),
                            BookDetails = reader["BookDetails"].ToString(),
                            BookQuantity = Convert.ToInt32(reader["BookQuantity"]),
                            BookImage = reader["BookImage"].ToString()
                        });
                    }
                    this.sqlConnection.Close();
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

        public bool DeleteBook(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spDeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
