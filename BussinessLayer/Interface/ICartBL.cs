using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICartBL
    {
        string AddBookToCart(AddToCart cartBook);
        string DeleteCart(int CartId);
        bool UpdateCart(int CartId, int BooksQty);
        List<CartModel> GetAllBooksinCart(int UserId);

    }
}
