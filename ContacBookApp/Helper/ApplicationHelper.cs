using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ContacBookApp.Helper
{
    public static class ApplicationHelper
    {

        public const string Website_URL = "WebsiteURL";
        public const string EncryptKey = "crM@!000";

        public static class EnumJQueryResponseType
        {
            public const string MessageOnly = "M";
            public const string MessageAndRedirect = "M-R";
            public const string MessageAndRedirectWithDelay = "M-R-D";
        }
        public class AjaxReponse
        {
            public string Message { get; set; }
            public bool Status { get; set; }
            public string Type { get; set; }
            public string RedirectURL { get; set; }
     
        }

        #region Helper Functions

        public static void AddCookie(string _key, string _value, int _numberOfHourAdd = 0)
        {
            System.Web.HttpCookie CookieObject = new System.Web.HttpCookie(_key);
            CookieObject.Value = _value;
            if (_numberOfHourAdd > 0)
            {
                CookieObject.Expires = DateTime.UtcNow.AddHours(_numberOfHourAdd);
            }
            HttpContext.Current.Response.Cookies.Add(CookieObject);
        }
        public static string GetCookie(string _key)
        {
            string ReturnValue = string.Empty;
            System.Web.HttpCookie CookieObject = HttpContext.Current.Request.Cookies[_key];
            if (CookieObject != null)
            {
                ReturnValue = CookieObject.Value;
            }
            return ReturnValue;
        }
        public static void RemoveCookie(string _key)
        {
            System.Web.HttpCookie CookieObject = HttpContext.Current.Request.Cookies[_key];
            if (CookieObject != null)
            {
                CookieObject.Expires = DateTime.UtcNow.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(CookieObject);
            }
        }
        public static byte[] GetKey
        {
            get
            {
                return ASCIIEncoding.ASCII.GetBytes(EncryptKey);
            }
        }
        public static string Encrypt(string _value)
        {
            string ReturnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(_value))
            {
                ReturnValue = EncryptString(_value, GetKey);
            }
            return ReturnValue;
        }
        private static string EncryptString(string _value, byte[] _bytes)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(_bytes, _bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(_value);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
        public static string Decrypt(string _value)
        {
            string ReturnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(_value))
            {
                ReturnValue = DecryptString(_value, GetKey);
            }
            return ReturnValue;
        }
        private static string DecryptString(string _value, byte[] _bytes)
        {
            _value = Regex.Replace(_value, "[ ]", "+");
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(_value));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(_bytes, _bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
        #endregion

        #region CustomFunctions
        public static string GetContentByKey(ContactEntities context,string Key)
        {
            string WebsiteURL = string.Empty;
            var record = context.Settings.FirstOrDefault(x => x.Keys.Equals(Key));
            if (record != null)
            {
                WebsiteURL = record.Contents;
            }
            return WebsiteURL;
        }

        public static bool IsUserLogin()
        {
            var UserId = GetCookie("CurrentUserId");
            if (!string.IsNullOrEmpty(UserId))
            {
                return true;
            }
            return false;
        }
       
        #endregion
    }
}