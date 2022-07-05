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
        //public WishListModel AddBookinWishList(WishListModel wishListModel, int userId)
        //{
        //    try
        //    {
        //        this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
        //        SqlCommand cmd = new SqlCommand("SPAddInWishlist", this.sqlConnection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        cmd.Parameters.AddWithValue("@BookId", wishListModel.BookId);
        //        cmd.Parameters.AddWithValue("@UserId", userId);
        //        sqlConnection.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);


                SqlCommand cmd = new SqlCommand("SPAddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (sqlConnection)
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", wishlistModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
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
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPDeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@WishListId", WishListId);

                this.sqlConnection.Open();
                int res = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (res == 0)
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
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPGetAllBooksinWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<AddToWishList> wishlistmodels = new List<AddToWishList>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            AddToWishList WishlistModel = new AddToWishList();
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

                        sqlConnection.Close();
                        return wishlistmodels;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
        }












        //public bool DeleteBookinWishList(int WishlistId, int userId)
        //{
        //    sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            SqlCommand cmd = new SqlCommand("SPDeleteFromWishlist", sqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@WishListId", WishlistId);
        //            cmd.Parameters.AddWithValue("@UserId", userId);
        //            sqlConnection.Open();
        //           int result= cmd.ExecuteNonQuery ();
        //            sqlConnection.Close();
        //            if(result!=0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}

        //public string DeleteBookinWishList(int WishListId, int UserId)
        //{
        //    try
        //    {
        //        this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
        //        SqlCommand cmd = new SqlCommand("SPDeleteFromWishlist", this.sqlConnection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        cmd.Parameters.AddWithValue("@WishListId", WishListId);

        //        this.sqlConnection.Open();
        //        int res = cmd.ExecuteNonQuery();
        //        this.sqlConnection.Close();
        //        if (res == 0)
        //        {
        //            return "succesful";
        //        }
        //        else
        //        {
        //            return "failed";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        this.sqlConnection.Close();
        //    }

        //}
    }
}
