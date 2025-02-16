using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class DBSingleton
    {
        private static SqlConnection? conn = null;

        private DBSingleton() { }


        public static SqlConnection GetInstance()
        {
            if (conn == null)
            {
                SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
                stringBuilder.InitialCatalog = ReadSetting("Database");
                stringBuilder.DataSource = ReadSetting("DataSource");
                stringBuilder.UserID = ReadSetting("UserID");
                stringBuilder.Password = ReadSetting("Password");
                stringBuilder.ConnectTimeout = 30;
                stringBuilder.TrustServerCertificate = true;
                stringBuilder.Encrypt = true;
                conn = new SqlConnection(stringBuilder.ConnectionString);
                conn.Open();
            }
            return conn;
        }

        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}
