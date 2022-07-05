using DatabaseLayer.Model;
using Microsoft.Extensions.Configuration;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ReposatoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string AddOrder(OrderModel orderModel, int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            //connecting the sql connection of book store
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPAddOrder", this.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;//command type is a class to set the store procedure
                    //adding parameter to store procedure
                    cmd.Parameters.AddWithValue("@OrderBookQuantity", orderModel.Quantity);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                    
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();// it execute the query for update ,insert or delete.
                    sqlConnection.Close();
                    if (result == 2)
                    {
                        return "Please Enter Correct Address TypeId For Adding Address";
                    }
                    else
                    {
                        return "Address Added Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ViewOrderModel> GetAllOrder(int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            //connecting the sql connection of book store
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPGetOrders", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;//command type is a class to set the store procedure
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewOrderModel> order = new List<ViewOrderModel>();
                        while (reader.Read())
                        {
                            ViewOrderModel orderModel = new ViewOrderModel();
                            BookModel bookModel = new BookModel();
                            orderModel.OrdersId = Convert.ToInt32(reader["OrdersId"]);
                            orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                            orderModel.BookId = Convert.ToInt32(reader["BookId"]);
                            orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                            orderModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                            orderModel.Quantity = Convert.ToInt32(reader["OrderBookQuantity"]);
                            orderModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.BookImage = reader["BookImage"].ToString();
                            orderModel.BookModel = bookModel;
                            order.Add(orderModel);
                        }

                        sqlConnection.Close();
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
