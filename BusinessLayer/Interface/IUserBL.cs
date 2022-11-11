using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserModel AddUser(UserModel user);
        public UserLogin LoginUser(string Email, string Password);
        public string ForgotPassword(string email);
    }
}
