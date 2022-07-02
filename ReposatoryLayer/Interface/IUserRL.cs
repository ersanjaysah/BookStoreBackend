using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistration Registration(UserRegistration userRegistration);
        public string UserLogin(UserLogin userLogin);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
