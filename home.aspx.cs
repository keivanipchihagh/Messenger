using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Messenger
{
    public partial class Home : System.Web.UI.Page
    {
        public const string connectionString = responder.connectionString;

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent Caching
            Response.Buffer = true;
            Response.CacheControl = "no-cache";
            Response.AddHeader("pragma", "no-cache");
            Response.Expires = -1;

            sqlConnection = new SqlConnection(connectionString);

            if (loadSelf())
            {
                addFriends(user_ID.Value);
                loadContacts();
            }
            else
                Response.Redirect("http://messenger.keivanipchihagh.ir/");
        }

        protected bool loadSelf()
        {
            if (Request.Form["email"] != null && Request.Form["pass"] != null /*true*/)
            {
                /* Get Activation Statue Of User */
                sqlCommand = new SqlCommand("SELECT * FROM Members WHERE Members_Email = @email AND Members_Password = @password", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@email", Request.Form["email"] /*"ipchi1380@gmail.com"*/));  // Add parameter
                sqlCommand.Parameters.Add(new SqlParameter("@password", getMD5Hash(Request.Form["pass"]) /*getMD5Hash("25251380")*/));  // Add parameter
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
            contacts.InnerHtml = "<div class=\"w3-bar-item w3-button menuItem\" style=\"min-width: max-content\"><input id=\"contactsSearch\" type=\"text\" style=\"height: 90%; width: 88%\" maxlength=\"50\" placeholder=\"Search Contacts\" oninput=\"filterContacts()\" /><i class=\"fa fa-refresh\" aria-hidden=\"true\" title=\"Refresh List\" style=\"padding-left: 5%\" onclick=\"loadContacts()\"></i></div>";
            while (dataReader.Read())
                contacts.InnerHtml += "<a id=\"" + dataReader["Members_ID"] + "\" class=\"w3-bar-item w3-button menuItem\" style=\"min-width: max-content\" title=\"Click to open chat\" onclick=\"getChat(this)\"><i class=\"fa fa-user\" style=\"padding-right: 10px\"></i>@" + dataReader["Members_UserName"] + " - Last Seen: " + dataReader["Members_LastActivity"] + " </a>";

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

        /***
        * Adds all registered users as friends for the new user
        */
        protected void addFriends(string MemberID)
        {
            // Get All IDs
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_ID != @ID ORDER BY Members_ID ASC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@ID", MemberID));  // Add parameter
            sqlConnection.Open();   // Open Connection

            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Execute
            List<string> IDs = new List<string>();
            while (dataReader.Read())
                IDs.Add(dataReader["Members_ID"].ToString());

            // Remove all friends
            sqlConnection.Close();
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("DELETE FROM Friendships WHERE Friendship_ID = @ID", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@ID", MemberID));  // Add parameter
            sqlConnection.Open();   // Open Connection
            sqlCommand.ExecuteNonQuery();            

            foreach (string ID in IDs)
            {
                sqlConnection.Close();  // Close Connection
                sqlCommand = new SqlCommand("INSERT INTO Friendships (Friendship_ID, Friendship_FriendID) VALUES (@ID, @FriendID)", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@ID", MemberID));  // Add parameter
                sqlCommand.Parameters.Add(new SqlParameter("@FriendID", ID));  // Add parameter
                sqlConnection.Open();   // Open Connection
                sqlCommand.ExecuteNonQuery();   // Execute
                sqlConnection.Close();  // Close Connection
            }
        }
    }
}