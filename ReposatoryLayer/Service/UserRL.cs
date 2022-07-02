using DatabaseLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReposatoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        //Iconfig.. represents the set of key-value application configuration property
        public UserRL(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        /// <summary>
        /// User registration method in UserRL class
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns></returns>
        public UserRegistration Registration(UserRegistration userRegistration)
        {
            //sql connection connectingstring to book store
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SPUserRegister", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure; //command type is a class to set the store procedure

                    cmd.Parameters.AddWithValue("@FullName", userRegistration.FullName);

                    cmd.Parameters.AddWithValue("@Email", userRegistration.Email);

                    cmd.Parameters.AddWithValue("@Password", userRegistration.Password);

                    cmd.Parameters.AddWithValue("@MobileNumber", userRegistration.MobileNumber);
                    // used to open the connection first
                    sqlConnection.Open();
                    //ExecuteNonQuery method is used to execute SQL Command for INSERT, UPDATE or Delete operations.
                    int result = cmd.ExecuteNonQuery();
                    // closing the connection
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return userRegistration;
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

        /// <summary>
        /// user login method in UserRL class
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public string UserLogin(UserLogin userLogin)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPUserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };

                cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                cmd.Parameters.AddWithValue("@Password", userLogin.Password);
                // opening the connection
                this.sqlConnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                // for checking datareader has row or not
                if (reader.HasRows)
                {
                    int UserId = 0;
                    UserLogin user = new UserLogin();

                    while (reader.Read())
                    {
                        user.Email = Convert.ToString(reader["Email"]);
                        user.Password = Convert.ToString(reader["Password"]);
                        UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    // closing the connection
                    this.sqlConnection.Close();
                    var Token = this.GenerateJWTToken(user.Email, UserId);
                    return Token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // finnally block at last it has to be execute
            finally
            {
                this.sqlConnection.Close();
            }
        }

        /// <summary>
        /// method used for generating JWT Token
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private string GenerateJWTToken(string Email, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),// valid for 2 hr

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Forgot Password method in UserRL class
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("SPUserForgetPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //command type is a class to set the store procedure
                };
                cmd.Parameters.AddWithValue("@Email", email);
                
                this.sqlConnection.Open();     //opening the connection
                
                SqlDataReader reader = cmd.ExecuteReader();  //execute all result to get all record
                if (reader.HasRows)   // for checking datareader has row or not
                {
                    int UserId = 0;
                    UserLogin user = new UserLogin(); //creating the obect of model class
                    
                    while (reader.Read())      //loop is used for read multiple rows
                    {
                        user.Email = Convert.ToString(reader["Email"]);
                        UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    this.sqlConnection.Close();   //connection opened

                    MessageQueue queue;
                    if (MessageQueue.Exists(@".\Private$\BookQueue"))  //Determine whether the queue exist in the given path .
                    {
                        queue = new MessageQueue(@".\Private$\BookQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookQueue");
                    }

                    Message MyMessage = new Message(); //connecting the message to local computer
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    // formatter serializes the object into the stream and insert it into the body
                    MyMessage.Body = this.GenerateJWTToken(email, UserId);
                    EmailService.SendMail(email, MyMessage.Body.ToString()); // sending the mail.
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReciveCompleted);
                    //Added event handler for receive completed event
                    var token = this.GenerateJWTToken(email, UserId);
                    return token;
                }
                else
                {
                    this.sqlConnection.Close(); // closing the connection
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close(); // at last it will run
            }
        }

        /// <summary>
        /// method provides an event handler for the receive completed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender; // connect to the queue.
                    Message msg = queue.EndReceive(e.AsyncResult); // here Ending the Asynchronous Receive operation.
                    EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString())); // sending the msg to email.
                    queue.BeginReceive(); // here Re-starting the Async receive operation.
                }
            }
            catch (MessageQueueException ex)
            {
            if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " + "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        }

        private string GenerateToken(string email)
        {
            if (email == null) // checking the condition
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler(); //initilizing a new instance of the jwtsecurityTokenHandler class
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor   //initilize a new instance for all the attribute related to the issued token
            {
                Subject = new ClaimsIdentity(new Claim[]  
                {
                    new Claim("Email", email),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                // initializes a new instance of the SigningCredentials class.
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
                //it contains the cryptographic key that is used to generate the digital signature
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Reset Password UserRL class
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                
                    this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                    if (newPassword == confirmPassword) // here checking the condition
                    {
                        SqlCommand command = new SqlCommand("SPUserResetPassword", this.sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure //Command type is a class to set as stored procedure
                        };

                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", confirmPassword);

                        this.sqlConnection.Open(); //opening the connection
                        int qr = command.ExecuteNonQuery(); //execute the query for update , insert , delete
                        this.sqlConnection.Close(); // closing the connection here
                        if (qr >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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
    }
}






    


