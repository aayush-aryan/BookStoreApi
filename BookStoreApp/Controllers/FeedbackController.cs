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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddFeedback(FeedBackModel feedbackModeld)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var feedback = this.feedbackBL.AddFeedback(feedbackModeld, userId);
                if (feedback != null)
                {
                    return this.Ok(new { Status = true, Message = " Successfully Feedback For This Book Added ", Response = feedback });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "  Enter Correct BookId!!!!" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetFeedback")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = this.feedbackBL.GetAllFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Successfully Feedback For Given Book Id Fetched ANd Displayed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
