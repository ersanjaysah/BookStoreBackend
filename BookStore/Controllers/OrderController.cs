using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL OrderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.OrderBL = orderBL;
        }

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderModel orderModel) //IActionResult is an interface that return
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.OrderBL.AddOrder(orderModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Order Added SuccessFully", response = result });
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

        [Authorize(Roles = Role.User)]//it authorization is the process to veryfy specific data,file a user has to access
        [HttpPost("GetAllOrder")]
        public IActionResult GetAllOrder() //IActionResult is an interface that return
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.OrderBL.GetAllOrder(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Order List fetched successful ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry! Failed to fetch" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

    }
}
