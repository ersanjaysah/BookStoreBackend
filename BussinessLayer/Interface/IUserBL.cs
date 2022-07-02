using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegistration Registration(UserRegistration userRegistration);
        public string UserLogin(UserLogin userLogin);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
