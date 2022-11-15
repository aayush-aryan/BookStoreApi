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
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddBookToCart")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddToCart(AddToCart cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
                var userData = this.cartBL.AddBookToCart(cart, userId);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added to cart Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Book Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeletebyCartId")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeletCart(int CartId)
        {
            try
            {
                var data = this.cartBL.DeleteCart(CartId);
                if (data != null)
                {
                    return this.Ok(new { Success = true, message = "Book in Cart Deleted Sucessfully", });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid CartId" }); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateCartByCartId")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                var Data = this.cartBL.UpdateCart(CartId, BooksQty);
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

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBooksInCart")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAllBookInCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "userID").Value);
                var result = this.cartBL.GetAllBooksinCart(userId);

                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "All Books Displayed in the cart Successfully", Response = result });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid UserId" }); }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
