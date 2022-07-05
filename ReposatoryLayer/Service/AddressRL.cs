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
    public class AddressRL : IAddressRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //connecting the sql connection of book store
                SqlCommand cmnd = new SqlCommand("SPAddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };
                //adding parameter to store procedure
                cmnd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmnd.Parameters.AddWithValue("@City", addressModel.City);
                cmnd.Parameters.AddWithValue("@State", addressModel.State);
                cmnd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                cmnd.Parameters.AddWithValue("@UserId", UserId);
                this.sqlConnection.Open();//opening the sql connection
                cmnd.ExecuteScalar(); //only returns the value from the first column of the first row of query.
                return " Address Added Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();// closing the sql connection
            }
        }

        public bool UpdateAddress(int AddressId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //connecting the sql connection of book store
                SqlCommand cmnd = new SqlCommand("SPUpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };
                // adding the parameter to store procedure
                cmnd.Parameters.AddWithValue("@AddressId", AddressId);
                cmnd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmnd.Parameters.AddWithValue("@City", addressModel.City);
                cmnd.Parameters.AddWithValue("@State", addressModel.State);
                cmnd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                this.sqlConnection.Open();//opening the connection
                cmnd.ExecuteScalar();//only returns the value from the first column of the first row of query.
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();//finally closing the connection
            }
        }

        public bool DeleteAddress(int AddressId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //connecting the sql connection of book store
                SqlCommand cmnd = new SqlCommand("SPDeleteAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };
                // adding the parameter to store procedure
                cmnd.Parameters.AddWithValue("@AddressId", AddressId);
                this.sqlConnection.Open();//opening the connection
                cmnd.ExecuteScalar();//only returns the value from the first column of the first row of query.
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close(); //closing the connection
            }
        }
    }
}
