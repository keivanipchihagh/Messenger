using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Messenger
{
    public partial class responder : System.Web.UI.Page
    {
        //public const string connectionString = "Data Source = .; Initial Catalog = Messenger; Integrated Security = True";
        public const string connectionString = "xxxxxxxxxxxxxxxxxxx";

        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        private Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent Caching
            Response.Buffer = true;
            Response.CacheControl = "no-cache";
            Response.AddHeader("pragma", "no-cache");
            Response.Expires = -1;

            try
            {
                sqlConnection = new SqlConnection(connectionString);    // Initialize connection

                switch (Request.QueryString["Action"])
                {
                    case "signup": signup(); break; // Sign Up
                    case "login": login(); break;   // Login

                    case "activate": activate(); break; // Activate Account From Acivation Email
                    case "resendActivationCode": resendActivation(); break;     // Recends The Acivation Email
                    case "checkActivationEmailExpiredDate": checkActivationEmailExpiredDate(); break;     // Checks Wether Link Has Been Expired Or Not

                    case "recoverPassword": recoverPassword(); break;   // Send A Recovery Password To The Inbox
                    case "redirectToRecoveryPage": redirectToRecoveryPage(); break; // Load & Setup The Recovery Password Page
                    case "resetPassword": resetPassword(); break;   // Update The Password From The Reset Email
                    case "checkReocveryPasswordExpiredDate": checkReocveryPasswordExpiredDate(); break;     // Checks Wether Link Has Been Expired Or Not

                    case "fetchLogs": fetchLogs(); break;    // Fetches most recent logs for the user signed in
                    case "getChat": getChat(); break;     // Loads the requested chat
                    case "sendMessage": sendMessage(); break;   // Sends a message to a friend ID
                    case "checkForIncomingMessages": getIncomingMessages(); break;  // Get Unread messages
                    case "isConnected": isConnected(); break;   // Checks if server is connected or not

                }
            }
            catch (Exception ex) { Response.Write(ex.Message); }    // Show Error Message
            finally { sqlConnection.Close(); /* Close Connection */ }
        }

        /***
         *  Creates & Returns an activation code based in user ID
         */
        protected string getActivationCode(string UserID)
        { return getRandomChar(UserID.ToCharArray()[0]) + "-" + getRandomChar(UserID.ToCharArray()[1]) + "-" + getRandomChar(UserID.ToCharArray()[2]) + "-" + getRandomChar(UserID.ToCharArray()[3]); }

        /***
         * Returns a random 4-Digit code where first digit is part of user ID
         */
        protected string getRandomChar(char firstChar)
        {
            char[] chars = "1234567890QWERTYUIOPLKJHGFDSAZXCVBNM".ToCharArray();
            return firstChar + chars[random.Next(0, chars.Length)].ToString() + chars[random.Next(0, chars.Length)].ToString() + chars[random.Next(0, chars.Length)].ToString();
        }

        /***
         * Sends the activation email
         */
        protected void sendActivationMail(string emailAddress, string activationCode)
        {
            MailMessage mailMessage = new MailMessage("noreply@keivanipchihagh.ir", emailAddress);
            mailMessage.Subject = "Messenger - Activation Code";
            mailMessage.Body = "<body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><style type=\"text/css\">@media screen {@font-face { font-family: 'Lato'; font-style: normal; font-weight: 400; src: local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff'); }@font-face {font-family: 'Lato'; font-style: normal; font-weight: 700; src: local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff'); }@font-face { font-family: 'Lato'; font-style: italic; font-weight: 400; src: local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff'); }@font-face { font-family: 'Lato'; font-style: italic; font-weight: 700; src: local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff'); }}body, table, td, a { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; }img { -ms-interpolation-mode: bicubic; }img { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; }table { border-collapse: collapse !important; }body {height: 100% !important; margin: 0 !important; padding: 0 !important; width: 100% !important; }a[x-apple-data-detectors] { color: inherit !important; text-decoration: none !important; font-size: inherit !important; font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important; }@media screen and (max-width:600px) { h1 { font-size: 32px !important; line-height: 32px !important; } }div[style*=\"margin: 16px 0;\"] { margin: 0 !important; }</style><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\">Thanks for using Messenger!</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#149ddd\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#149ddd\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Thanks for using <i style=\"color: #149ddd\"><b>Messenger!</b></i></h1> <img src=\" https://img.icons8.com/clouds/100/000000/handshake.png\" width=\"125\" height=\"120\" style=\"display: block; border: 0px;\" /></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">We will keep you posted with up coming features and newsletter once you activate your account. </p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 0px 30px 20px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" style=\"border-radius: 7px;\" bgcolor=\"#149ddd\"><a href=\"messenger.keivanipchihagh.ir/responder.aspx?Action=activate&Email=" + Request.QueryString["Email"] + "&ActivationCode=" + activationCode + "\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; display: inline-block\">Activate Your Account</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">If you did not make this request, please ignore this email. This activation code expires in 24 hours.</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">With regards,<br /><i>Mesenger Team</i></p></td></tr></table></td></tr></table></body></html>";
            mailMessage.IsBodyHtml = true;  // Set as HTML format

            SmtpClient smtpClient = new SmtpClient("webmail.keivanipchihagh.ir");
            smtpClient.Credentials = new NetworkCredential("noreply@keivanipchihagh.ir", "Keivan25251380");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMessage);
        }

        /***
         * Sends the recovery email
         */
        protected void sendRecoveryMail(string emailAddress, string activationCode)
        {
            MailMessage mailMessage = new MailMessage("noreply@keivanipchihagh.ir", emailAddress);
            mailMessage.Subject = "Messenger - Password Recovery";
            mailMessage.Body = "<body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><meta name=\"viewport\" content=\"width =device-width, initial-scale=1\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\" /><style type=\"text/css\">@media screen {@font-face {font-family: 'Lato';font-style: normal;font-weight: 400;src: local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff');}@font-face {font-family: 'Lato';font-style: normal;font-weight: 700;src: local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff');}@font-face {font-family: 'Lato';font-style: italic;font-weight: 400;src: local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff');}@font-face {font-family: 'Lato';font-style: italic;font-weight: 700;src: local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff');}}body, table, td, a {-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;}table, td {mso-table-lspace: 0pt;mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}img {border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;}table {border-collapse: collapse !important;}body {height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: none !important;font-size: inherit !important;font-family: inherit !important;font-weight: inherit !important;line-height: inherit !important;}@media screen and (max-width:600px) {h1 {font-size: 32px !important;line-height: 32px !important;}}div[style*=\"margin: 16px 0;\"] {margin: 0 !important;}</style><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato' , Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\">Password Recovery - Messenger!</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#149ddd\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#149ddd\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Password Recovery <i style=\"color: #149ddd\"><b>Messenger</b></i></h1></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">We have received a request to reset your password; We are here to help! </p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 0px 30px 20px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" style=\"border-radius: 7px;\" bgcolor=\"#149ddd\"><a href=\"messenger.keivanipchihagh.ir/responder.aspx?Action=redirectToRecoveryPage&Email=" + Request.QueryString["Email"] + "&ActivationCode=" + activationCode + "\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; display: inline-block\">Reset Your Password</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">If you did not make this request, don't worry; Your password is still safe and you can simply ignore this email. This link expires in 24 hours.</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">With regards,<br /><i>Mesenger Team</i></p></td></tr></table></td></tr></table></body>";
            mailMessage.IsBodyHtml = true;  // Set as HTML format

            SmtpClient smtpClient = new SmtpClient("webmail.keivanipchihagh.ir");
            smtpClient.Credentials = new NetworkCredential("noreply@keivanipchihagh.ir", "Keivan25251380");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMessage);
        }

        /***
         * Login
         */
        protected void login()
        {
            /* Check For Bans */
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT TOP 1 * FROM Logs WHERE Log_StaticIP = @IP AND Log_AuthenticationType = 'Ban' AND Log_AuthenticationResult = 'Banned' ORDER BY Log_DateTime DESC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@IP", GetIPAddress()));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Execute

            if (dataReader.Read())  // Chech If Ban Exists On The IP
            {
                // Get Request Date & Time
                string date = dataReader["Log_DateTime"].ToString().Split('-')[0];
                string time = dataReader["Log_DateTime"].ToString().Split('-')[1];

                DateTime requestedSpan = new DateTime(int.Parse(date.Split(':')[0]), int.Parse(date.Split(':')[1]), int.Parse(date.Split(':')[2]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), int.Parse(time.Split(':')[2]));
                DateTime nowSpan = DateTime.Now;
                TimeSpan differece = nowSpan - requestedSpan;

                if (differece.TotalMinutes <= 15)   // Still Banned
                {
                    Response.Write("Code 4");
                    return;
                }
            }

            /* Check Login Attempts */
            sqlConnection.Close();  // Close Connection
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT TOP 5 * FROM Logs WHERE Log_AuthenticationType = 'Login' AND Log_StaticIP = @IP ORDER BY Log_DateTime DESC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@IP", GetIPAddress()));  // Add parameter
            sqlConnection.Open();   // Open Connection
            dataReader = sqlCommand.ExecuteReader();  // Execute

            bool succeedLogin = false;
            int failedattemps = 0;
            while (dataReader.Read())
            {
                // Get Request Date & Time
                string date = dataReader["Log_DateTime"].ToString().Split('-')[0];
                string time = dataReader["Log_DateTime"].ToString().Split('-')[1];

                if (dataReader["Log_AuthenticationResult"].ToString() == "Granted")
                    succeedLogin = true;

                DateTime requestedSpan = new DateTime(int.Parse(date.Split(':')[0]), int.Parse(date.Split(':')[1]), int.Parse(date.Split(':')[2]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), int.Parse(time.Split(':')[2]));
                DateTime nowSpan = DateTime.Now;
                TimeSpan differece = nowSpan - requestedSpan;

                if (differece.TotalMinutes <= 15)
                    failedattemps++;
            }

            if (failedattemps == 5 && !succeedLogin)   // 5 Failure Attempts
            {
                writeLog("Banned IP", "Ban", "Banned");
                Response.Write("Code 3");
            }
            else
            {
                /* Check If Email Exists Or Not */
                sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
                sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email", sqlConnection);  // Intialize Command
                sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                sqlConnection.Open();
                dataReader = sqlCommand.ExecuteReader();  // Execute

                bool recordExists = false;
                string ID = "Unknown";

                if (dataReader.Read())  // If Email Exists, Save ID
                {
                    ID = dataReader["Members_ID"].ToString();
                    recordExists = true;
                }
                sqlConnection.Close();  // Close Connection

                /* Get Activation Statue Of User */
                sqlCommand = new SqlCommand("SELECT Members_IsActivated FROM Members WHERE Members_Email = @email AND Members_Password = @password", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
                sqlConnection.Open();   // Open connection

                dataReader = sqlCommand.ExecuteReader();    // Execute
                if (dataReader.Read())  // Records Found
                    if (dataReader["Members_IsActivated"].ToString() == "true")     // User Exist & Is Activated
                    {
                        Response.Write("Code 1");   // Code 1: 'User exists'
                        if (recordExists) writeLog(ID, "Login", "Granted");     // User Exists But Not Activated
                    }
                    else
                    {
                        Response.Write("Code 2");   // Code 1: 'Account has not been activated yet'
                        if (recordExists) writeLog(ID, "Login", "Denied");
                    }
                else    // Record Not Found
                {
                    Response.Write("Code 0");   //  Code 0: 'User does not exist'
                    writeLog(ID, "Login", "Denied");
                }
            }
        }

        /***
         * Determines Connectivity
         */
        protected void isConnected()
        {
            Response.Write("true");
        }

        /***
         * Get Unread Messages
         */
        protected void getIncomingMessages()
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT * FROM Messages WHERE (Message_ReceiverID = @selfID AND Message_SenderID = @friendID AND Message_Status = 'unread') ORDER BY Message_DateTime ASC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@selfID", Request.QueryString["SelfID"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@friendID", Request.QueryString["FriendID"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            string content = "";
            while (dataReader.Read())
                content += "<div class=\"chatBox chatBox-left\"><div class=\"chatBox-container-left\"><p class=\"chatBox-content\">" + dataReader["Message_Content"] + "</p><div class=\"chatBox-time-container-left\"><span class=\"chatBox-time\">" + dataReader["Message_DateTime"] + "</span></div></div></div>";

            // Set Messages as read
            sqlConnection.Close();
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("UPDATE Messages SET Message_Status = 'read' WHERE (Message_ReceiverID = @selfID AND Message_SenderID = @friendID AND Message_Status = 'unread')", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@selfID", Request.QueryString["SelfID"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@friendID", Request.QueryString["FriendID"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            sqlCommand.ExecuteNonQuery();  // Receive Results

            Response.Write(content);
        }

        /***
         * Writes Log Info
         */
        protected void writeLog(string MemberID, string AuthType, string AuthResult)
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT MAX(Log_ID) AS MAXLOGID FROM Logs", sqlConnection);     // Initialize Command
            sqlConnection.Open();    // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Execute

            int logID = 0;
            try   // Try And Get Maximum Existing Log ID
            {
                if (dataReader.Read())
                    logID = int.Parse(dataReader["MAXLOGID"].ToString());
            }
            catch (Exception) { }
            sqlConnection.Close();  // Close Connection


            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("INSERT INTO Logs (Log_MemberID, Log_StaticIP, Log_Location, Log_DateTime, log_AuthernticationID, Log_AuthenticationType, Log_AuthenticationResult) VALUES (@ID, @IP, @Loc, @DateTime, @AuthID, @AuthType, @AuthResult)", sqlConnection);   // Initialize Command
            sqlCommand.Parameters.Add(new SqlParameter("@ID", MemberID));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@IP", GetIPAddress()));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@Loc", "UNKNOWN"));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@DateTime", DateTime.Now.ToString("yyyy:MM:dd - HH:mm:ss:ff")));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@AuthID", getMD5Hash(logID + "-" + MemberID)));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@AuthType", AuthType));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@AuthResult", AuthResult));  // Add parameter
            sqlConnection.Open();   // Open Connection
            sqlCommand.ExecuteNonQuery();   // Execute
            sqlConnection.Close();  // Close Connection
        }

        /***
         * Sign Up
         */
        protected void signup()
        {
            /* Check If UserName Is Taken */
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_UserName = @username", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@username", Request.QueryString["UserName"]));  // Add parameter
            sqlConnection.Open();   // Open Connection

            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Execute
            if (dataReader.Read())  // If Username Exists
                Response.Write("Code 0");   // Code 0: 'Another user with the same username exists'
            else   // Ussername Does Not Exist
            {
                /* Check If Email Is Taken */
                sqlConnection.Close();  // Close Connection
                sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                sqlConnection.Open();   // Open Connection

                dataReader = sqlCommand.ExecuteReader();    // Execute
                if (dataReader.Read())  // If Email Exists
                    Response.Write("Code 2");   // Code 0: 'Another user with the same email exists'
                else    // Email Does Not Exist
                {
                    /* Get Max Member_ID */
                    sqlConnection.Close();  // Close Connection
                    sqlCommand = new SqlCommand("SELECT MAX(Members_ID) AS Members_ID FROM Members", sqlConnection);    // Initialize 
                    sqlConnection.Open();   // Open Connection
                    dataReader = sqlCommand.ExecuteReader();    // Execute

                    // Generate Activation Code
                    string activationCode = null;
                    if (dataReader.Read())
                        activationCode = getActivationCode(Convert.ToString(Convert.ToInt32(dataReader["Members_ID"]) + 1));

                    sendActivationMail(Request.QueryString["Email"], activationCode); // Send Activation Email

                    /* Insert Data Into Databse */
                    sqlConnection.Close();  // Close Connection
                    sqlCommand = new SqlCommand("INSERT INTO Members (Members_FullName, Members_UserName, Members_Email, Members_Password, Members_ActivationCode, Members_IsActivated) VALUES (@fullname, @username, @email, @password, @activationCode, 'false')", sqlConnection);    // Initialize command
                    sqlCommand.Parameters.Add(new SqlParameter("@fullname", Request.QueryString["FullName"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@username", Request.QueryString["UserName"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@activationCode", activationCode));  // Add parameter
                    sqlConnection.Open();   // Open Connection
                    sqlCommand.ExecuteNonQuery();   // Execute
                    sqlConnection.Close();  // Close Connection                    

                    sqlCommand = new SqlCommand("SELECT MAX(Members_ID) AS MAXMEMBERID FROM Members", sqlConnection);    // Initialize command
                    sqlConnection.Open();   // Open Connection
                    dataReader = sqlCommand.ExecuteReader();   // Insert

                    string MemberID = "0";
                    try
                    {
                        if (dataReader.Read())
                            MemberID = dataReader["MAXMEMBERID"].ToString();    // Get Max Member ID
                    }
                    catch (Exception) { }
                    sqlConnection.Close();  // Close Connection

                    addFriends(MemberID);

                    writeLog(MemberID, "Sign Up", "Created");   // Write Sign Up Log                   

                    Response.Write("Code 1");   // Code 1: 'Insertion & activation was successfull'
                }
            }
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

        /***
         * Activate Account
         */
        protected void activate()
        {
            int dateStatus;
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT * FROM Logs WHERE Logs.Log_AuthenticationType = 'Sign Up' AND Logs.Log_MemberID = (SELECT Members.Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode) ORDER BY Log_DateTime ASC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Execute

            if (dataReader.Read())  // Request Exists
            {
                // Get Requset Date & Time
                string date = dataReader["Log_DateTime"].ToString().Split('-')[0];
                string time = dataReader["Log_DateTime"].ToString().Split('-')[1];

                DateTime requestedSpan = new DateTime(int.Parse(date.Split(':')[0]), int.Parse(date.Split(':')[1]), int.Parse(date.Split(':')[2]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), int.Parse(time.Split(':')[2]));
                DateTime nowSpan = DateTime.Now;
                TimeSpan differece = nowSpan - requestedSpan;

                if (differece.TotalDays >= 1)
                    dateStatus = 2;  // Link Is Expired
                else
                    dateStatus = 1;   // All Good, Not Expired Yet
            }
            else
                dateStatus = 0;   // No Requested Recovery Emails -404

            if (dateStatus == 1)
            {
                sqlConnection.Close();
                sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
                sqlCommand = new SqlCommand("SELECT Members_IsActivated, Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
                sqlConnection.Open();   // Open Connection
                dataReader = sqlCommand.ExecuteReader();  // Execute

                if (dataReader.Read())
                    if (dataReader["Members_IsActivated"].ToString() == "false")    // Not Activated
                    {
                        string MemberID = dataReader["Members_ID"].ToString();  // Get Account ID
                                                                                /***
                                                                                 * Activate Account
                                                                                 */
                        sqlConnection.Close();  // Close Connection
                        sqlCommand = new SqlCommand("UPDATE Members SET Members_IsActivated = 'true'", sqlConnection);    // Initialize command
                        sqlConnection.Open();   // Open Connection
                        sqlCommand.ExecuteNonQuery();

                        // Write Activation Log
                        writeLog(MemberID, "Activation", "Activated");

                        Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Yey! Your account has been activated successfully! You may login now...</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Login</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
                    }
                    else   // Already Activated
                        Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Account has already been activated. You may login now...</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Login</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
                else   // No Records Found
                    Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Link not found (Err 404). Request one from Here:</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Request New Link</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");

                sqlConnection.Close();
            }
            else if (dateStatus == 2) // Link Expired
                Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">You are 24 hours late, Link has been expired. Want to request a new one?</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Request New Link</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
            else   // Link Not Found
                Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Link not found (Err 404) - Probably expired. Request one from Here:</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Request New Link</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
        }

        /***
         * Resend Activation
         */
        protected void resendActivation()
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT Members_ActivationCode FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            if (dataReader.Read())  // User Info Is Correct
            {
                try
                {
                    sendActivationMail(Request.QueryString["Email"], dataReader["Members_ActivationCode"].ToString());  // Send Email
                }
                catch (Exception) { }
                Response.Write("Code 1");
            }
            else
                Response.Write("Code 2");
        }

        /***
         * Recover Password - Does Not Alert User If The Email Is Correct Or Not
         */
        protected void recoverPassword()
        {
            // Get Activation Code Using Email
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT Members_ActivationCode, Members_ID FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Execute

            if (dataReader.Read())  // Record Exists
            {
                try
                {
                    sendRecoveryMail(Request.QueryString["Email"], dataReader["Members_ActivationCode"].ToString());   // Send Recovery
                    writeLog(dataReader["Members_ID"].ToString(), "Password Recovery", "Requested");
                }
                catch (Exception) { Response.Write("Code 0"); }
            }
            Response.Write("Code 1");   // Record Not Found
        }

        /***
         * Request Password Recovery - Email Redirect Page
         */
        protected void redirectToRecoveryPage()
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            if (dataReader.Read())  // Request Exists
                Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Reset Password</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default//main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default//style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><script src=\"assets/js/main.js\"></script></head><body onload=\"checkPasswordRecoveryExpireDate()\"><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div id=\"content\" style=\"background-color: white; text-align: center; padding: 20px\"><input id=\"identifier\" name=\"identifier\" type=\"hidden\" value=\"login\" /><h3 class=\"legend\" style=\"font-weight: bold\">~ Reset Password ~</h3><div id=\"alertBox\"></div><input id=\"email\" name=\"email\" type=\"hidden\" value=\"" + Request.QueryString["Email"] + "\" /><input id=\"code\" name=\"code\" type=\"hidden\" value=\"" + Request.QueryString["ActivationCode"] + "\" /><div class=\"input\"><span class=\"fa fa-key\" aria-hidden=\"true\"></span><input type=\"password\" placeholder=\"New Password\" name=\"pass\" id=\"pass\" required=\"required\" maxlength=\"50\" /><i class=\"fa fa-eye\" aria-hidden=\"true\" title=\"Show/Hide Password\" onclick=\"this.classList.toggle('fa-eye-slash'); changePassVisual()\"></i></div><div class=\"input\"><span class=\"fa fa-key\" aria-hidden=\"true\"></span><input type=\"password\" placeholder=\"Confirm New Password\" name=\"confirm\" id=\"confirm\" required=\"required\" maxlength=\"50\" /></div><button type=\"submit\" class=\"btn submit\" onclick=\"resetPassword()\">Reset</button><a href=\"http://messenger.keivanipchihagh.ir/\" class=\"bottom-text-w3ls\" style=\"margin-top: 22px; cursor: pointer\">Remember Your Password?</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div></body></html>");
            else      // Request Does Not Exist
                Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Reset Password</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/default//main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/default//style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><script src=\"assets/js/main.js\"></script></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div id=\"content\" style=\"background-color: white; text-align: center; padding: 20px\"><h3 class=\"legend\" style=\"font-weight: bold\">~ Reset Password ~</h3>This link contains Invalid signeratures. Session has been terminated due to EA violations; Your link might be expired. Please request a new link form:<a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Request New Link</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div></body></html>");
        }

        /***
         * Sends a message to a friend ID
         */
        protected void sendMessage()
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("INSERT INTO Messages (Message_SenderID, Message_ReceiverID, Message_Content, Message_DateTime, Message_Status, Message_Trace) VALUES (@selfID, @friendID, @content, @dateTime, 'unread', 'N/A')", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@selfID", Request.QueryString["SelfID"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@friendID", Request.QueryString["FriendID"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@content", Request.QueryString["Content"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@dateTime", DateTime.Now.ToString("yyyy:MM:dd - HH:mm:ss:ff")));  // Add parameter

            sqlConnection.Open();   // Open Connection
            sqlCommand.ExecuteNonQuery();  // Receive Results

            Response.Write("<div class=\"chatBox chatBox-right\"><div class=\"chatBox-container-right\"><p class=\"chatBox-content\">" + Request.QueryString["Content"] + "</p><div class=\"chatBox-time-container-right\"><span class=\"chatBox-time\">" + DateTime.Now.ToString("HH:mm:ss") + "</span></div></div></div>");
        }

        /***
         * Checks Whether 24 Hours Have Passed Since Password Recovery Request
         */
        protected void checkReocveryPasswordExpiredDate()
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT * FROM Logs WHERE Logs.Log_AuthenticationType = 'Password Recovery' AND Logs.Log_AuthenticationResult = 'Requested' AND Logs.Log_MemberID = (SELECT Members.Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode) ORDER BY Log_DateTime ASC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            if (dataReader.Read())  // Requst Exists
            {
                // Get Request Date & Time
                string date = dataReader["Log_DateTime"].ToString().Split('-')[0];
                string time = dataReader["Log_DateTime"].ToString().Split('-')[1];

                DateTime requestedSpan = new DateTime(int.Parse(date.Split(':')[0]), int.Parse(date.Split(':')[1]), int.Parse(date.Split(':')[2]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), int.Parse(time.Split(':')[2]));
                DateTime nowSpan = DateTime.Now;
                TimeSpan differece = nowSpan - requestedSpan;

                if (differece.TotalDays >= 1)
                    Response.Write("Code 2");   // Link Is Expired
                else
                    Response.Write("Code 1");   // All Good, Not Expired Yet
            }
            else
                Response.Write("Code 0");   // No Requested Recovery Emails - 404
        }

        /***
         * Checks Whether 24 Hours Have Passed Since Sign Up
         */
        protected bool checkActivationEmailExpiredDate()
        {
            /* Get Sign Up Information */
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT * FROM Logs WHERE Logs.Log_AuthenticationType = 'Sign Up' AND Logs.Log_MemberID = (SELECT Members.Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode) ORDER BY Log_DateTime ASC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            if (dataReader.Read())  // Request Exists
            {
                // Get Date & Time
                string date = dataReader["Log_DateTime"].ToString().Split('-')[0];
                string time = dataReader["Log_DateTime"].ToString().Split('-')[1];

                DateTime requestedSpan = new DateTime(int.Parse(date.Split(':')[0]), int.Parse(date.Split(':')[1]), int.Parse(date.Split(':')[2]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), int.Parse(time.Split(':')[2]));
                DateTime nowSpan = DateTime.Now;
                TimeSpan differece = nowSpan - requestedSpan;

                if (differece.TotalDays >= 1)   // More Than 24 Hours Have Passed
                {
                    Response.Write("Code 2");
                    return false;  // Link Is Expired
                }
                else
                    return true;   // All Good, Not Expired Ye
            }
            else    // Request Does Not Exists
            {
                Response.Write("Code 0");
                return false;   // No Requested Recovery Emails -404
            }
        }

        /***
         * Reset Password
         */
        protected void resetPassword()
        {
            /* Write Log */
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT * FROM Logs WHERE Logs.Log_AuthenticationType = 'Password Recovery' AND Logs.Log_AuthenticationResult = 'Requested' AND Logs.Log_MemberID = (SELECT Members.Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode)", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            if (dataReader.Read())  // Request Exists
                writeLog(dataReader["Log_MemberID"].ToString(), "Password Recovery", "Recovered");
            else    // Request Does Not Exist
                writeLog(dataReader["Log_MemberID"].ToString(), "Password Recovery", "Denied");

            /* Update The Password */
            sqlConnection.Close();  // Close Connection
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("UPDATE Members SET Members_Password = @password WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection

            if (sqlCommand.ExecuteNonQuery() >= 1)
                Response.Write("Code 1");   // Success
            else
                Response.Write("Code 0");   // Failed
        }

        /***
         * Get Chat
         */
        protected void getChat()
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT * FROM Messages WHERE (Message_SenderID = @selfID AND Message_ReceiverID = @friendID) OR (Message_ReceiverID = @selfID AND Message_SenderID = @friendID) ORDER BY Message_DateTime ASC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@selfID", Request.QueryString["SelfID"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@friendID", Request.QueryString["FriendID"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            string content = "";
            while (dataReader.Read())
            {
                if (dataReader["Message_SenderID"].ToString() == Request.QueryString["SelfID"])
                    content += "<div class=\"chatBox chatBox-right\"><div class=\"chatBox-container-right\"><p class=\"chatBox-content\">" + dataReader["Message_Content"] + "</p><div class=\"chatBox-time-container-right\"><span class=\"chatBox-time\">" + dataReader["Message_DateTime"].ToString().Split('-')[1].Substring(0, 9) + "</span></div></div></div>";
                else
                    content += "<div class=\"chatBox chatBox-left\"><div class=\"chatBox-container-left\"><p class=\"chatBox-content\">" + dataReader["Message_Content"] + "</p><div class=\"chatBox-time-container-left\"><span class=\"chatBox-time\">" + dataReader["Message_DateTime"].ToString().Split('-')[1].Substring(0, 9) + "</span></div></div></div>";
            }

            if (content == "")
                content = "Don't be shy, start a conversation.";

            Response.Write("<div style=\"width: 100%; height: 50px\"><div style=\"width: 30%; float: left\"><h6 style=\"color: white; margin: 0px; border-bottom: 1px solid rgb(0, 148, 254)\"><i class=\"fa fa-weixin\" aria-hidden=\"true\" style=\"padding: 10px; color: rgb(0, 148, 254); font-size: 20px\"></i>" + getFullName(Request.QueryString["FriendID"]) + " </h6></div><div style=\"width: 30%; text-align: end; float: right\"><h6 style=\"color: white; margin: 0px; border-bottom: 1px solid rgb(0, 148, 254); font-size: 20px\">" + getFullName(Request.QueryString["SelfID"]) + "<i class=\"fa fa-weixin\" aria-hidden=\"true\" style=\"padding: 10px; color: rgb(0, 148, 254)\"></i></h6></div></div>" + content);
        }

        /***
         * Fetch Logs
         */
        protected void fetchLogs()
        {
            /* Write Log */
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT TOP 15 * FROM Logs WHERE Log_MemberID = @ID ORDER BY Log_DateTime DESC", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@ID", Request.QueryString["ID"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            string content = "";
            while (dataReader.Read())
            {
                content += "<tr><td>" + dataReader["Log_AuthenticationType"] + "</td>";
                switch (dataReader["Log_AuthenticationResult"])
                {
                    case "Created": content += "<td style=\"color: blue\">" + dataReader["Log_AuthenticationResult"] + "</td>"; break;
                    case "Activated":
                    case "Recovered":
                    case "Granted": content += "<td style=\"color: green\">" + dataReader["Log_AuthenticationResult"] + "</td>"; break;
                    case "Denied": content += "<td style=\"color: red\">" + dataReader["Log_AuthenticationResult"] + "</td>"; break;
                    case "Banned": content += "<td style=\"color: red; font-weight: bold\">" + dataReader["Log_AuthenticationResult"] + "</td>"; break;
                    default: content += "<td>" + dataReader["Log_AuthenticationResult"] + "</td>"; break;
                }
                content += "<td>" + dataReader["Log_DateTime"] + "</td><td>" + dataReader["Log_StaticIP"] + "</td><td>" + dataReader["log_AuthernticationID"] + "</td></tr>";
            }

            Response.Write("<h2 style=\"margin: 0px; font-weight: bold\"><i class=\"fa fa-line-chart\" style=\"padding-right: 7px\" aria-hidden=\"true\"></i>Recent Activities</h2><table><tr><th>Authentication</th><th>Result</th><th>Date & Time</th><th>IP Address</th><th>Trace</th></tr>" + content + " </table>");
        }

        /***
         * Get Client IP Address (Proxy Proof)
         */
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
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
         * Get FullName Using ID
         */
        protected string getFullName(string ID)
        {
            sqlConnection = new SqlConnection(connectionString);    // Initialize Connection
            sqlCommand = new SqlCommand("SELECT Members_FullName FROM Members WHERE Members_ID = @ID", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@ID", ID));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();  // Receive Results

            if (dataReader.Read())
                return dataReader["Members_FullName"].ToString();
            else
                return null;
        }
    }
};