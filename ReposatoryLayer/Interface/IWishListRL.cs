using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface IWishListRL
    {
       // public WishListModel AddBookinWishList(WishListModel wishlistModel, int UserId);

        // public bool DeleteBookinWishList(int WishlistId, int userId);
        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId);
        public string DeleteWishList(int WishListId, int UserId);

        public List<AddToWishList> GetWishlistByUserid(int UserId);

        // public string DeleteWishList(int WishListId, int UserId);



    }
}
