﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.model
{
   public class UserModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
    }
}
