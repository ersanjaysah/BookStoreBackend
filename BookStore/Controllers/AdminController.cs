using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(AdminResponse adminResponse)//IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var result = this.adminBL.Adminlogin(adminResponse);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login Failed", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
