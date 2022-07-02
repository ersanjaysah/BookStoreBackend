using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [ApiController] // used to binding the source by manually by applying attribute
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registration method
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>

         [HttpPost("Registration")]
        public IActionResult Registration(UserRegistration userRegistration)
        {
            try
            {
                UserRegistration userData = this.userBL.Registration(userRegistration);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Sorry! User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Login method controller class
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>

        [HttpPost("UserLogin")]
        public IActionResult UserLogin(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.UserLogin(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", Token = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Sorry!!! Login Failed", Token = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Forgot password controller class
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var token = this.userBL.ForgotPassword(email);
                if (token != null)
                {
                    return this.Ok(new { Success = true, message = " Mail Sent Successful", Response = token });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Reset password controller class
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("ResetPassword/{newPassword}/{confirmPassword}")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
               // var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Changed Unsuccessfully " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
