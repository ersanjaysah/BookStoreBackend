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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public string AddBookToCart(AddToCart cartBook)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPAddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@UserId", cartBook.UserId);
                cmd.Parameters.AddWithValue("@BookId", cartBook.BookId);
                cmd.Parameters.AddWithValue("@BooksQty", cartBook.BooksQty);

                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return "book added in cart successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }

        }

        public string DeleteCart(int CartId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spDeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", CartId);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return "Book Deleted in Cart Successfully";
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

        public bool UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spUpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@BooksQty", BooksQty);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
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
