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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public BookModel AddBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPAddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure ////Command type is a class to set as stored procedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@TotalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@BookDetails", book.BookDetails);
                cmd.Parameters.AddWithValue("@BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("@BookQuantity", book.BookQuantity);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();// it execute the query for update ,insert or delete.
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return book;
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
                this.sqlConnection.Close();
            }
        }

       

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPGetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel book = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                book.BookName = reader["BookName"].ToString();
                book.AuthorName = reader["AuthorName"].ToString();
                book.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                book.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                book.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                book.BookDetails = reader["BookDetails"].ToString();
                book.BookImage = reader["BookImage"].ToString();
                book.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);

                return book;
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

        public bool UpdateBook(int BookId, BookModel updateBook)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPUpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookId", BookId);
                cmd.Parameters.AddWithValue("@BookName", updateBook.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", updateBook.AuthorName);
                cmd.Parameters.AddWithValue("@TotalRating", updateBook.TotalRating);
                cmd.Parameters.AddWithValue("@RatingCount", updateBook.RatingCount);
                cmd.Parameters.AddWithValue("@DiscountPrice", updateBook.DiscountPrice);
                cmd.Parameters.AddWithValue("@OriginalPrice", updateBook.OriginalPrice);
                cmd.Parameters.AddWithValue("@BookDetails", updateBook.BookDetails);
                cmd.Parameters.AddWithValue("@BookImage", updateBook.BookImage);
                cmd.Parameters.AddWithValue("@BookQuantity", updateBook.BookQuantity);
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
                this.sqlConnection.Close();
            }
        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPDeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPGetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["BookName"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            TotalRating = Convert.ToInt32(reader["TotalRating"]),
                            RatingCount = Convert.ToInt32(reader["RatingCount"]),
                            DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]),
                            BookDetails = reader["BookDetails"].ToString(),
                            BookImage = reader["BookImage"].ToString(),
                            BookQuantity = Convert.ToInt32(reader["BookQuantity"])
                        });
                    }
                    this.sqlConnection.Close();
                    return book;
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
                this.sqlConnection.Close();
            }
        }
    }
}
