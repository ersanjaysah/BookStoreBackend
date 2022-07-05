using BussinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController] // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize(Roles = Role.User)] //it authorization is the process to veryfy specific data,file a user has to access
        [HttpPost("addAddress/{UserId}")]
        public IActionResult AddAddress(int UserId, AddressModel addressModel) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var result = this.addressBL.AddAddress(UserId, addressModel);
                if (result.Equals(" Address Added Successfully"))
                {
                    return this.Ok(new { success = true, message = $"Address Added Successfully " });
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
        [HttpPut("updateAddress/{AddressId}")]// it allow you to edit existing HTTP resource
        public IActionResult UpdateAddress(int AddressId, AddressModel addressModel) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var result = this.addressBL.UpdateAddress(AddressId, addressModel);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address updated Successfully " });
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

        [Authorize(Roles = Role.User)]////it authorization is the process to veryfy specific data,file a user has to access
        [HttpDelete("DeleteAddress/{AddressId}")]//// it allow you to delete existing HTTP resource
        public IActionResult DeleteAddress(int AddressId) //IActionResult is an interface that return multiple type of data.
        {
            try
            {
                var result = this.addressBL.DeleteAddress(AddressId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address deleted Successfully " });
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
