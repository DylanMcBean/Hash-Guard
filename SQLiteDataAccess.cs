using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Password_Manager
{
    public class SQLiteDataAccess
    {
        public static UserClass LoadUser(string usernameToFind)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UserClass>($"select * from user_data where username='{usernameToFind}'", new DynamicParameters());
                try
                {
                    UserClass user = new UserClass();
                    user.username = output.First().username;
                    user.passwordHash = output.First().passwordHash;
                    user.encryptedData = output.First().encryptedData == null ? null : output.First().encryptedData.ToArray();
                    return user;
                }
                catch (Exception e)
                {
                    return new UserClass();
                }
            }
        }

        public static void SaveUser(UserClass user, bool overwriteData)
        {
            try
            {
                if (overwriteData)
                {
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Execute("update user_data set encryptedData=@encryptedData WHERE username=@username", user);
                    }
                }
                else
                {
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Execute("insert into user_data (username, passwordHash, encryptedData) values (@username, @passwordHash, @encryptedData)", user);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
