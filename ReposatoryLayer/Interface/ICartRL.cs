using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface ICartRL
    {
        string AddBookToCart(AddToCart cartBook);
        string DeleteCart(int CartId);
        bool UpdateCart(int CartId, int BooksQty);

    }
}
