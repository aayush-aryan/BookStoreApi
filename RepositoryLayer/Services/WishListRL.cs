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
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public string AddBookinWishList(AddToWishList wishListModel, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spAddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", wishListModel.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return "book is added in WishList successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<WishListModel> GetAllBooksinWishList(int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spGetAllBooksinWishList", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<WishListModel> wishList = new List<WishListModel>();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WishListModel model = new WishListModel();
                        BookModel bookModel = new BookModel();
                        model.UserId = Convert.ToInt32(reader["UserId"]);
                        model.WishListId = Convert.ToInt32(reader["WishListId"]);
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
                        model.bookModel = bookModel;
                        wishList.Add(model);
                    }
                    return wishList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteBookinWishList(int WishListId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand cmd = new SqlCommand("spDeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@WishListId", WishListId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                //  cmd.ExecuteScalar();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;  
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
