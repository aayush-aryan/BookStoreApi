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
   public class FeedbackRL : IFeedbackRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BooKStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    cmd.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    return feedbackModel;  
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
