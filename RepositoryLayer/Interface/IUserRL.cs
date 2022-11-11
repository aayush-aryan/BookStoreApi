﻿using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserModel AddUser(UserModel user);
        public UserLogin LoginUser(string Email, string Password);
    }
}
