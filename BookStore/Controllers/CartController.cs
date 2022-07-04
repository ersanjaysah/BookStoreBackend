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
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpPost("AddBookToCart")] //HttpPost is used to send the data to server from Http client.
        public IActionResult AddToCart(AddToCart cart) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var userData = this.cartBL.AddBookToCart(cart);
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

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpDelete("Delete/{CartId}")]
        public IActionResult DeletCart(int CartId) //IActionResult is an interface that return multiple type of data.
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

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpPut("UpdateCart/{CartId}/{BooksQty}")]
        public IActionResult UpdateCart(int CartId, int BooksQty) //IActionResult is an interface that return multiple type of data.
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

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpGet("GetAllBooksInCart/{userId}")]
        public IActionResult GetAllBookInCart(int userId) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
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
