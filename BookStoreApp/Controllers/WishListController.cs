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
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishlistBL;

        public WishListController(IWishListBL wishListBL)
        {
            this.wishlistBL = wishListBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("addBooksInWishList")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddBookinWishList(AddToWishList wishListModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = this.wishlistBL.AddBookinWishList(wishListModel, userId);
                if (result.Equals("book is added in WishList successfully"))
                {
                    return this.Ok(new { success = true, message = $"Book is added in WishList  Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBooksinWishList")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAllBooksinWishList()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = this.wishlistBL.GetAllBooksinWishList(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"All Books Displayed in the WishList Successfully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Books are not there in WishList " });
                }
            }
            catch (Exception eX)
            {
                throw eX;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteBookinWishList")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteBookinWishList(int WishListId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = this.wishlistBL.DeleteBookinWishList(WishListId, userId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book is deleted from the WishList " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
