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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
       
        public string AddBookToCart(AddToCart cartBook, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spAddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BooksQty", cartBook.BooksQty);
                cmd.Parameters.AddWithValue("@BookId", cartBook.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();  
                return "book added in cart successfully";
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

        public string DeleteCart(int CartId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spDeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", CartId);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return "Book Deleted in Cart Successfully";
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spUpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@BooksQty", BooksQty);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<CartModel> GetAllBooksinCart(int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spGetAllBookinCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<CartModel> cart = new List<CartModel>();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CartModel model = new CartModel();
                        BookModel bookModel = new BookModel();
                        model.CartId = Convert.ToInt32(reader["CartId"]);
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        bookModel.PeopleRating = Convert.ToInt32(reader["PeopleRating"]);
                        bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetails = reader["BookDetails"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                        bookModel.BookImage = reader["BookImage"].ToString();
                        model.BookId = Convert.ToInt32(reader["BookId"]);
                        model.BooksQty = Convert.ToInt32(reader["BooksQty"]);
                        model.bookModel = bookModel;
                        cart.Add(model);
                    }
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
    }
}
