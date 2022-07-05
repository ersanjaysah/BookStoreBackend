﻿using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IOrderBL
    {
        public string AddOrder(OrderModel orderModel, int userId);
        public List<ViewOrderModel> GetAllOrder(int userId);
    }
}
