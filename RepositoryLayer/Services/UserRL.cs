using CommonLayer.model;
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
                    //else
                    //{
                    //    //this.sqlConnection.Close();
                    //    return null;
                    //}
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
    }
}
