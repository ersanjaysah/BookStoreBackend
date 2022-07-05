using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class ViewOrderModel
    {
        public int OrdersId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int BookId { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? OrderDate { get; set; }
        public BookModel BookModel { get; set; }
    }
}
