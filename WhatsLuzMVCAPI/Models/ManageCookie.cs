using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    public class ManageCookie
    {
        
        public static UserAccount user = null;
        public static bool isAdmin()
        {
                user = CheckCookieExists();
                if (user == null)
                    return false;
                return (user.isAdmin == 1);                    
        }
        
        public static UserAccount CheckCookieExists()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["FacebookCookie"];
            if (cookie == null)
                return null;

            String SHA256Hash = cookie.Value.ToString();
            var dataContext = new SqlConnectionDataContext();
            user = getUserByHash(dataContext, SHA256Hash);
            return user;
            

        }

        public static string SHA256Hash(string fid)
        {
            string SALT = "eL!@d&H@y&S@99!e"; // For Encryption SALT with the facebook user id

            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] byteD2e = uEncode.GetBytes(fid + SALT);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(byteD2e);

            return Convert.ToBase64String(hash);
        }

        public static HttpCookie CreateCookie(string fidHash)
        {
            HttpCookie StudentCookies = new HttpCookie("FacebookCookie");


            StudentCookies.Value = fidHash;
            StudentCookies.Expires = DateTime.Now.AddHours(1);
            return StudentCookies;
        }

        private static UserAccount getUserByHash(SqlConnectionDataContext db,string SHA256Hash)
        {
            UserAccount usera =
   (from u in db.UserAccounts
    where u.Hash == SHA256Hash
    select u).FirstOrDefault();
            return usera;
        }
    }
}