using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class WishListBL : IWishListBL
    {
       private readonly IWishListRL wishlistRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishlistRL = wishListRL;
        }


        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId)
        {
            try
            {
                return this.wishlistRL.AddToWishList(wishlistModel, UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                return this.wishlistRL.DeleteWishList(WishListId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AddToWishList> GetWishlistByUserid(int UserId)
        {
            try
            {
                return this.wishlistRL.GetWishlistByUserid(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }










        //public WishListModel AddBookinWishList(WishListModel wishListModel, int userId)
        //{
        //    try
        //    {
        //        return this.wishlistRL.AddBookinWishList(wishListModel, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        //public bool DeleteBookinWishList(int WishlistId, int userId)
        //{
        //    try
        //    {
        //        return this.wishlistRL.DeleteBookinWishList(WishlistId, userId);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


    }
}
