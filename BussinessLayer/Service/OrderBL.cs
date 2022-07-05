using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL OrderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.OrderRL = orderRL;
        }
        public string AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                return this.OrderRL.AddOrder(orderModel, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ViewOrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this.OrderRL.GetAllOrder(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
