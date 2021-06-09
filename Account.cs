using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Password_Manager
{
    [Serializable()]
    public class Account
    {
        public string name {get; set;}
        public string url { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string notes { get; set; }
        public string mainName
        {
            get
            {
                return getMainAccountName(name);
            }
        }
        public string uuid { get; set; }

        public Account(string name, string url, string email, string username, string password, string notes)
        {
            this.name = name;
            this.email = email;
            this.username = username;
            this.password = password;
            this.notes = notes;
        }

        private string getMainAccountName(string accountName)
        {
            accountName = accountName.ToLower();
            accountName = Regex.Replace(accountName, @"\b(\w)", m => m.Value.ToUpper());
            accountName = Regex.Replace(accountName, @"(\sAccount|Account\s)","");
            accountName = Regex.Replace(accountName, @"(\s\d+|\d+\s)", "");

            return accountName;
        }
    }
}
