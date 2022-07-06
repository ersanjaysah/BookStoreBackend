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
    public class FeedbackRL : IFeedbackRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
            //connecting the sql connection to the Bookstore
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPAddFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    cmd.Parameters.AddWithValue("@TotalRating", feedbackModel.TotalRating);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();//it will insert ,delete,update the data
                    sqlConnection.Close();
                    return feedbackModel;
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

        public List<DisplayFeedback> GetAllFeedback(int bookId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPGetFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<DisplayFeedback> feedbackModel = new List<DisplayFeedback>();
                        while (reader.Read())
                        {
                            DisplayFeedback getFeedback = new DisplayFeedback();
                            UserRegistration user = new UserRegistration
                            {
                                FullName = reader["FullName"].ToString()
                            };

                            getFeedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                            getFeedback.Comment = reader["Comment"].ToString();
                            getFeedback.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                            getFeedback.BookId = Convert.ToInt32(reader["BookId"]);
                            getFeedback.User = user;
                            feedbackModel.Add(getFeedback);
                        }
                        sqlConnection.Close();
                        return feedbackModel;
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
            finally
            {
                sqlConnection.Close();
            }
        }
    }
    
}
