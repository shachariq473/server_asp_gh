using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Farmers
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int Phone_Number { get; set; }

        public Farmers(string userId, string userName, string userEmail, string userPassword, int phone_Number)
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
            Phone_Number = phone_Number;
        }

        public Farmers()
        {
        }

        public override string ToString()
        {
            return $"{UserId}, {UserName}, {UserEmail}, {UserPassword}, {Phone_Number}";
        }
    }
    //SHACHAR\SQLEXPRESS
}