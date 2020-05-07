using System;
using System.Data.SqlClient;

namespace Messenger
{
    public partial class responder : System.Web.UI.Page
    {
        public const string connectionString = "Data Source = .; Initial Catalog = Messenger; Integrated Security = True";
        //public const string connectionString = "Data Source=www.keivanipchihagh.ir;Initial Catalog = xxxxxxxx; Persist Security Info=True;User ID = xxxxxxxxxxx; Password=xxxxxxxxxxxxxxxx";
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);    // Initialize connection

                switch (Request.QueryString["Action"])
                {
                    case "signup":                        
                        sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_UserName = @username", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@username", Request.QueryString["UserName"]));  // Add parameter
                        sqlConnection.Open();   // Open connection

                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        if (dataReader.Read())
                            Response.Write("Code 0");   // Code 0: 'Another user with the same username exists'
                        else
                        {
                            sqlConnection.Close();  // Close Connection
                            sqlCommand = new SqlCommand("INSERT INTO Members (Members_FullName, Members_UserName, Members_Email, Members_Password) VALUES (@fullname, @username, @email, @password)", sqlConnection);    // Initialize command
                            sqlCommand.Parameters.Add(new SqlParameter("@fullname", Request.QueryString["FullName"]));  // Add parameter
                            sqlCommand.Parameters.Add(new SqlParameter("@username", Request.QueryString["UserName"]));  // Add parameter
                            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                            sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
                            sqlConnection.Open();   // Open connection

                            sqlCommand.ExecuteNonQuery();   // Insert
                            Response.Write("Code 1");   // Code 1: 'Insertion was successfull'
                        }

                        break;
                }
            }
            catch (Exception ex) { Response.Write(ex.Message); }
            finally
            {
                sqlConnection.Close();  // Close Connection 
            }
        }
    }
}