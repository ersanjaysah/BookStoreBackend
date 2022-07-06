using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")] //Controller is a class that handels the HTTP request

    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }


        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedBackModel feedbackmodel) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.feedbackBL.AddFeedback(feedbackmodel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Feedback added successfull", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Oops!!! Unable to add Feedback" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetFeedback/{BookId}")]
        public IActionResult GetFeedback(int bookId) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var result = this.feedbackBL.GetAllFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Successfully Feedback For Given BookId Fetched and Displayed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
