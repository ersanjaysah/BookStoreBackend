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
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
      
        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //connecting the sql connection of book store
                SqlCommand cmd = new SqlCommand("SPAddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure  //command type is a class to set the store procedure
                };
                using (sqlConnection)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //adding parameter to store procedure
                    cmd.Parameters.AddWithValue("@BookId", wishlistModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();   //opening the connection
                    int result = cmd.ExecuteNonQuery();// it execute the query for update ,insert or delete.
                    sqlConnection.Close(); //closeing the connection
                    if (result != 0)
                    {
                        return wishlistModel;
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
        }

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //connecting the sql connection of book store
                SqlCommand cmd = new SqlCommand("SPDeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure  //command type is a class to set the store procedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@WishListId", WishListId);

                this.sqlConnection.Open();  //opening the connection
                int res = cmd.ExecuteNonQuery();// it execute the query for update ,insert or delete.
                this.sqlConnection.Close();   //closing the connection
                if (res == 0) //checking the condition
                {
                    return "succesful";
                }
                else
                {
                    return "failed";
                }
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

        public List<AddToWishList> GetWishlistByUserid(int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //connecting the sql connection of book store
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPGetAllBooksinWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;  //command type is a class to set the store procedure
                                                                    //adding parameter to store procedure
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(); // it returns the object that can iterate the entire result set.
                    if (reader.HasRows) // for checking datareader has row or not.
                    {
                        List<AddToWishList> wishlistmodels = new List<AddToWishList>();//create a instance of a list
                        while (reader.Read()) //call read before accessing data
                        {
                            BookModel bookModel = new BookModel(); //creating the instance of book model
                            AddToWishList WishlistModel = new AddToWishList(); // creating the instance of Add to wish list
                            bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.OriginalPrice = Convert.ToInt32(reader["originalPrice"]);
                            bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.BookImage = reader["BookImage"].ToString();
                            WishlistModel.UserId = Convert.ToInt32(reader["UserId"]);
                            WishlistModel.BookId = Convert.ToInt32(reader["BookId"]);
                            WishlistModel.WishListId = Convert.ToInt32(reader["WishListId"]);
                            WishlistModel.bookModel = bookModel;
                            wishlistmodels.Add(WishlistModel);
                        }
                        sqlConnection.Close(); // closing the connection
                        return wishlistmodels;
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
        }
    }
}
