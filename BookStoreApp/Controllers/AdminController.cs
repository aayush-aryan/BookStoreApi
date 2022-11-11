using BusinessLayer.Interface;
using CommonLayer.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }
        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(AdminResponse adminResponse)
        {
            try
            {
                var result = this.adminBL.Adminlogin(adminResponse);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login Failed", data = result });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
