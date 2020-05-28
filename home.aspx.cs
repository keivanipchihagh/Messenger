using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Messenger
{
    public partial class Home : System.Web.UI.Page
    {
        //public const string connectionString = responder.connectionString;
        public const string connectionString = responder.connectionString;

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);

            if (loadSelf())
                loadContacts();
            else
                Response.Redirect("http://messenger.keivanipchihagh.ir/");
        }

        protected bool loadSelf()
        {
            if (/*Request.Form["email"] != null && Request.Form["pass"] != null*/ true)
            {
                /* Get Activation Statue Of User */
                sqlCommand = new SqlCommand("SELECT * FROM Members WHERE Members_Email = @email AND Members_Password = @password", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@email", /*Request.Form["email"]*/ "ipchi1380@gmail.com"));  // Add parameter
                sqlCommand.Parameters.Add(new SqlParameter("@password", getMD5Hash(/*Request.Form["pass"]*/ "25251380")));  // Add parameter
                sqlConnection.Open();   // Open connection

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    user_ID.Value = dataReader["Members_ID"].ToString();
                    user_fullname.Value = dataReader["Members_FullName"].ToString();
                    fullnameBox.InnerHtml = " Welcome, <i>" + dataReader["Members_FullName"].ToString() + "</i>";
                    user_username.Value = dataReader["Members_UserName"].ToString();
                    user_email.Value = dataReader["Members_Email"].ToString();

                    sqlConnection.Close();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        protected void loadContacts()
        {
            /* Get Activation Statue Of User */
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("SELECT * FROM Friendships INNER JOIN Members ON Friendships.Friendship_FriendID = Members.Members_ID WHERE Friendships.Friendship_ID = @ID", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@ID", user_ID.Value));  // Add parameter
            sqlConnection.Open();   // Open connection

            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
                contacts.InnerHtml += "<a id=\"" + dataReader["Members_ID"] + "\" class=\"w3-bar-item w3-button menuItem\" style=\"min-width: max-content\" title=\"Click to open chat\" onclick=\"getChat(this)\"><i class=\"fa fa-user\" style=\"padding-right: 10px\"></i>" + dataReader["Members_FullName"] + " - Last Seen: " + dataReader["Members_LastActivity"] + " </a>";

            sqlConnection.Close();
        }

        /***
         * MD5 Hashing Algorithm
         */
        protected string getMD5Hash(string input)
        {
            // Calculate MD5 Hash
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Convert Byte Array Into Hex String 
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }
    }
}