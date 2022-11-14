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

        [Authorize(Roles = Role.User)]
        [HttpGet("GetBookById")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetBookByBookId(int BookId) 
        {
            try
            {
                BookModel userData = this.bookBL.GetBookByBookId(BookId);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "This is the Book which You wanted", Response = userData });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid BookId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBooks")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAllBook()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();

                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = result });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid BookId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeletebyBooKId")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]  
        public IActionResult DeletBook(int BookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(BookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully", });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid BookId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBook")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateBook(int BookId, BookModel Model)
        {
            try
            {
                var Data = this.bookBL.UpdateBook(BookId, Model);
                if (Data == true)
                {
                    return this.Ok(new { Success = true, message = " Book Updated successfully", Response = Data });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid BookId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
