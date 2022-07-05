using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface IWishListRL
    {
        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId);
        public string DeleteWishList(int WishListId, int UserId);
        public List<AddToWishList> GetWishlistByUserid(int UserId);
    }
}
