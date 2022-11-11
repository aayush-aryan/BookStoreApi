using BusinessLayer.Interface;
using CommonLayer.model;
using Microsoft.AspNetCore.Authorization;
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
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                BookModel userData = this.bookBL.AddBook(book);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Book Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
