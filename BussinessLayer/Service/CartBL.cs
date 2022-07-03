using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public string AddBookToCart(AddToCart cartBook)
        {
            try
            {
                return this.cartRL.AddBookToCart(cartBook);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteCart(int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId, BooksQty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartModel> GetAllBooksinCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBooksinCart(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
