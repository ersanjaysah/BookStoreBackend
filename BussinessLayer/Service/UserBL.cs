using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL :IUserBL //here inheriting the interface
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;

        }

       /// <summary>
       /// User Registration UserBL class
       /// </summary>
       /// <param name="userRegistration"></param>
       /// <returns></returns>

        public UserRegistration Registration(UserRegistration userRegistration)
        {
            try
            {
                return this.userRL.Registration(userRegistration);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// User Login UserBL class
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public string UserLogin(UserLogin userLogin)
        {
            try
            {
                return this.userRL.UserLogin(userLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Forgot password UserBL class
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Reset Password UserBL class
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
