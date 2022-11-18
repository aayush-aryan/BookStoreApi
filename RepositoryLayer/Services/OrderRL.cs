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
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public OrderModel AddOrder(OrderModel orderModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BooKStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderBookQuantity", orderModel.Quantity);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                    sqlConnection.Open();
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlConnection.Close();
                    if (i == 3)
                    {
                        return null;
                    }

                    if (i == 2)
                    {
                        return null;
                    }
                    else
                    {
                        return orderModel;
                    }

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

        public string CancelOrder(int OrdersId)
        {
            
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BooKStoreDb"));
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spCancelOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrdersId", OrdersId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "order Deleted Successfully";
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
