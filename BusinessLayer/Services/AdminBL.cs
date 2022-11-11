using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class AdminBL: IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminLoginModel Adminlogin(AdminResponse adminResponse)
        {
            try
            {
                return this.adminRL.Adminlogin(adminResponse);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
