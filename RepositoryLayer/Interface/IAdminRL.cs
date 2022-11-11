using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IAdminRL
    {
        public AdminLoginModel Adminlogin(AdminResponse adminResponse);
    }
}
