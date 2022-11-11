using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel AddUser(UserModel user)
        {

            try
            {
                return this.userRL.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserLogin LoginUser(string Email, string Password)
        {
            try
            {
                return this.userRL.LoginUser(Email,Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
