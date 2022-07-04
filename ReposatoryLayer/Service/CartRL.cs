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
                //connecting the sql connection of book store
                SqlCommand cmd = new SqlCommand("SPAddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure   //command type is a class to set the store procedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@UserId", cartBook.UserId);
                cmd.Parameters.AddWithValue("@BookId", cartBook.BookId);
                cmd.Parameters.AddWithValue("@BooksQty", cartBook.BooksQty);

                this.sqlConnection.Open(); // opening the SQL connection
                cmd.ExecuteNonQuery(); // it execute the query for update ,insert or delete.
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
                // here connecting the SQL connection to the book store
                SqlCommand cmd = new SqlCommand("spDeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };
                //Adding parameter to store procedure
                cmd.Parameters.AddWithValue("@CartId", CartId);
                sqlConnection.Open(); //opening the connection
                cmd.ExecuteScalar(); //it only returns the value from first column of the first row of the query.
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
                // here connecting the SQL connection to the book store.
                SqlCommand cmd = new SqlCommand("spUpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure 
                };
                //Adding parameter to store procedure
                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@BooksQty", BooksQty);
                sqlConnection.Open();  //opening the sql connection
                cmd.ExecuteScalar();  //It only returns the value of first row of first column of the query
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

        public List<CartModel> GetAllBooksinCart(int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                // here connecting the SQL connection to the book store.
                SqlCommand cmd = new SqlCommand("SPGetAllBookinCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };

                List<CartModel> cart = new List<CartModel>(); //creating the new instance of list of cartmodel class
                cmd.Parameters.AddWithValue("@UserId", UserId); //adding parameter to store procedure
                sqlConnection.Open();  // opening the connection of sql 
                SqlDataReader reader = cmd.ExecuteReader(); // it returns the object that can iterate the entire result set.
                if (reader.HasRows) // for checking datareader has row or not.
                {
                    while (reader.Read()) //call read before accessing data
                    {
                        CartModel model = new CartModel(); //creating new instance of cart model class
                        BookModel bookModel = new BookModel(); //creating new instance of book model class
                        model.CartId = Convert.ToInt32(reader["CartId"]);
                        bookModel.BookName = reader["BookName"].ToString();//reader is used to read the record at a time.
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                        bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetails = reader["BookDetails"].ToString();
                        bookModel.BookImage = reader["BookImage"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                        model.BookId = Convert.ToInt32(reader["BookId"]);
                        model.BooksQty = Convert.ToInt32(reader["BooksQty"]);
                        model.bookModel = bookModel;
                        cart.Add(model);
                    }
                    return cart;
                }
                else
                {
                    return null;
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
