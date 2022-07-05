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
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishlistBL;

        public WishListController(IWishListBL wishListBL)
        {
            this.wishlistBL = wishListBL;
        }

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpPost("AddToWishList")]
        public IActionResult AddToWishList(WishListModel wishlistModel) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.wishlistBL.AddToWishList(wishlistModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added SuccessFully in the wishlist ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to add book in the wishlist" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpDelete("DeleteWishList/{WishListId}")]
        public IActionResult DeleteWishList(int WishListId, int userId) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                
                var result = this.wishlistBL.DeleteWishList(WishListId, userId);
                if (result != null)
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

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpGet("GetWishlistByUserid/{UserId}")]
        public IActionResult GetWishlistByUserid(int UserId) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var wishlistdata = this.wishlistBL.GetWishlistByUserid(UserId);
                if (wishlistdata != null)
                {
                    return this.Ok(new { Success = true, message = "wishlist Detail Fetched Sucessfully", Response = wishlistdata });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter valid userId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }


        }


    }
}
