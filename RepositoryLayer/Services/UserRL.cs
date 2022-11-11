using CommonLayer.model;
using Experimental.System.Messaging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public UserRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public UserModel AddUser(UserModel user)
        {
            {
                try
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                    SqlCommand cmd = new SqlCommand("SP_UserRegister", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    this.sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (result != 0)
                    {
                        return user;
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
           public  UserLogin LoginUser(string Email, string Password)
            {
                UserLogin user = new UserLogin();
                try
                {

                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                    SqlCommand com = new SqlCommand("spUserLogin", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure 
                    };

                    com.Parameters.AddWithValue("@Email", Email);
                    com.Parameters.AddWithValue("@Password", Password);

                    this.sqlConnection.Open();
                    SqlDataReader rdr = com.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        int UserId = 0;
                        
                        while (rdr.Read()) 
                        {
                            user.Email = Convert.ToString(rdr["Email"]);
                            user.Password = Convert.ToString(rdr["Password"]);
                            UserId = Convert.ToInt32(rdr["UserId"]);
                        }
                        this.sqlConnection.Close();
                        user.Token = this.GetJWTToken(user.Email, UserId);
                        return user;
                    }
                   
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.sqlConnection.Close();  //Always ensuring the closing of the connection
                }
                return user;
           }


        private string GetJWTToken(string email, int userId)
        {
            //throw new NotImplementedException();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Configuration["key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("email", email),
                    new Claim("userID",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }


        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                SqlCommand com = new SqlCommand("spUserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);

                this.sqlConnection.Open();

                SqlDataReader reader = com.ExecuteReader(); // Execute sqlDataReader to fetching all records
                if (reader.HasRows)  
                {
                    int userId = 0;
                    UserLogin user = new UserLogin();
                    while (reader.Read()) 
                    {
                        email = Convert.ToString(reader["Email"]);
                        userId = Convert.ToInt32(reader["UserId"]);
                    }
                    this.sqlConnection.Close();

                    MessageQueue queue;

                    if (MessageQueue.Exists(@".\Private$\BookQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\BookQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = this.GetJWTToken(email, userId);
                    EmailService.SendMail(email, MyMessage.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReciveCompleted);

                    var token = this.GetJWTToken(email, userId);

                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void msmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender;
                    Message msg = queue.EndReceive(e.AsyncResult);
                    EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                    queue.BeginReceive();
                }

            }
            catch (MessageQueueException ex)
            {

                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        }

        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStoreDb"]);
                    SqlCommand com = new SqlCommand("spUserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure 
                    };

                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);

                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }  
                    else
                    {
                        return false;
                    }
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
