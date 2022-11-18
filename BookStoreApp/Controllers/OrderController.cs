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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
               // var userData = this.cartBL.AddBookToCart(cart, userId);
                var cartData = this.orderBL.AddOrder(orderModel, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Order Added SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Order Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeletebyOrderId")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeletOrder(int OrdersId)
        {
            try
            {
                var data = this.orderBL.CancelOrder(OrdersId);
                if (data != null)
                {
                    return this.Ok(new { Success = true, message = "Order Deleted Sucessfully", });
                }
                else { return this.BadRequest(new { Success = false, message = "Enter Valid OrderId" }); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
