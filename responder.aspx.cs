using System;
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
        public const string connectionString = "xxxxxxxxxxxxxxxxxxxxxxxx";
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Optimize Cache
            Response.Buffer = true;
            Response.CacheControl = "no-cache";
            Response.AddHeader("pragma", "no-cache");
            Response.Expires = -1;

            try
            {
                sqlConnection = new SqlConnection(connectionString);    // Initialize connection

                switch (Request.QueryString["Action"])
                {
                    case "signup": signup(); break;
                    case "login": login(); break;
                    case "activate": activate(); break;
                    case "resendActivationCode": resendActivation(); break;
                    case "recoverPassword": recoverPassword(); break;
                    case "requestPasswordRecovery": requestPasswordRecovery(); break;
                    case "resetPassword": resetPassword(); break;
                }
            }
            catch (Exception ex) { Response.Write(ex.Message); }
            finally { sqlConnection.Close(); /* Close Connection */ }
        }

        /***
         *  Returns an activation code based in user ID
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
            mailMessage.Body = "<body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><meta name=\"viewport\" content=\"width =device-width, initial-scale=1\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE =edge\" /><style type=\"text/css\">@media screen {@font-face {font-family: 'Lato';font-style: normal;font-weight: 400;src: local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff');}@font-face {font-family: 'Lato';font-style: normal;font-weight: 700;src: local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff');}@font-face {font-family: 'Lato';font-style: italic;font-weight: 400;src: local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff');}@font-face {font-family: 'Lato';font-style: italic;font-weight: 700;src: local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff');}}body, table, td, a {-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;}table, td {mso-table-lspace: 0pt;mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}img {border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;}table {border-collapse: collapse !important;}body {height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: none !important;font-size: inherit !important;font-family: inherit !important;font-weight: inherit !important;line-height: inherit !important;}@media screen and (max-width:600px) {h1 {font-size: 32px !important;line-height: 32px !important;}}div[style*=\"margin: 16px 0;\"] {margin: 0 !important;}</style><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato' , Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\">Password Recovery - Messenger!</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#149ddd\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#149ddd\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Password Recovery <i style=\"color: #149ddd\"><b>Messenger</b></i></h1></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">We have received a request to reset your password; We are here to help! </p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 0px 30px 20px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" style=\"border-radius: 7px;\" bgcolor=\"#149ddd\"><a href=\"messenger.keivanipchihagh.ir/responder.aspx?Action=requestPasswordRecovery&Email=" + Request.QueryString["Email"] + "&ActivationCode=" + activationCode + "\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; display: inline-block\">Reset Your Password</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">If you did not make this request, don't worry; Your password is still safe and you can simply ignore this email. This link expires in 24 hours.</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato' , Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0;\">With regards,<br /><i>Mesenger Team</i></p></td></tr></table></td></tr></table></body>";
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
            sqlConnection = new SqlConnection(connectionString);
            /***
             * Check If Email Exists Or Not
             */
            sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email", sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlConnection.Open();
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            bool recordExists = false;
            string ID = "Unknown";
            if (dataReader.Read())
            {
                ID = dataReader["Members_ID"].ToString();
                recordExists = true;
            }
            sqlConnection.Close();

            sqlCommand = new SqlCommand("SELECT Members_IsActivated FROM Members WHERE Members_Email = @email AND Members_Password = @password", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
            sqlConnection.Open();   // Open connection
            string s = Request.QueryString["Password"];
            dataReader = sqlCommand.ExecuteReader();
            if (dataReader.Read())
                if (dataReader["Members_IsActivated"].ToString() == "true")
                {
                    Response.Write("Code 1");   // Code 1: 'User exists'
                    if (recordExists) writeLog(ID, "Granted");
                }
                else
                {
                    Response.Write("Code 2");   // Code 1: 'Account has not been activated yet'
                    if (recordExists) writeLog(ID, "Denied");
                }
            else
            {
                Response.Write("Code 0");   //  Code 0: 'User does not exist'
                writeLog(ID, "Denied");
            }
        }

        /***
         * Writes Log Info
         */
        protected void writeLog(string ID, string Auth)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("INSERT INTO Logs (Log_MemberID, Log_StaticIP, Log_Location, Log_DateTime, Log_Authentication) VALUES (@ID, @IP, @Loc, @DateTime, @Auth)", sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@ID", ID));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@IP", GetIPAddress()));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@Loc", "UNKNOWN"));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@DateTime", DateTime.Now.ToString("yyyy:MM:dd - HH:mm:ss:ff")));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@Auth", Auth));  // Add parameter
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /***
         * Sign Up
         */
        protected void signup()
        {
            /***
             * Check If UserName Is Taken
             */
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_UserName = @username", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@username", Request.QueryString["UserName"]));  // Add parameter
            sqlConnection.Open();   // Open Connection

            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            if (dataReader.Read())
                Response.Write("Code 0");   // Code 0: 'Another user with the same username exists'
            else
            {
                sqlConnection.Close();  // Close Connection
                sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
                sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                sqlConnection.Open();   // Open Connection

                dataReader = sqlCommand.ExecuteReader();
                if (dataReader.Read())
                    Response.Write("Code 2");   // Code 0: 'Another user with the same email exists'
                else
                {
                    /***
                     * Get Max Member_ID
                     */
                    sqlConnection.Close();  // Close Connection
                    sqlCommand = new SqlCommand("SELECT MAX(Members_ID) AS Members_ID FROM Members", sqlConnection);    // Initialize 
                    sqlConnection.Open();   // Open Connection
                    dataReader = sqlCommand.ExecuteReader();

                    /***
                     * Generate Activation Code
                     */
                    string activationCode = null;
                    if (dataReader.Read())
                        activationCode = getActivationCode(Convert.ToString(Convert.ToInt32(dataReader["Members_ID"]) + 1));

                    sendActivationMail(Request.QueryString["Email"], activationCode); // Send Activation Email

                    /***
                     * Insert Data Into Databse
                     */
                    sqlConnection.Close();  // Close Connection
                    sqlCommand = new SqlCommand("INSERT INTO Members (Members_FullName, Members_UserName, Members_Email, Members_Password, Members_ActivationCode, Members_IsActivated) VALUES (@fullname, @username, @email, @password, @activationCode, 'false')", sqlConnection);    // Initialize command
                    sqlCommand.Parameters.Add(new SqlParameter("@fullname", Request.QueryString["FullName"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@username", Request.QueryString["UserName"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@password", getMD5Hash(Request.QueryString["Password"])));  // Add parameter
                    sqlCommand.Parameters.Add(new SqlParameter("@activationCode", activationCode));  // Add parameter
                    sqlConnection.Open();   // Open Connection
                    sqlCommand.ExecuteNonQuery();   // Insert
                    sqlConnection.Close();  // Close Connection

                    Response.Write("Code 1");   // Code 1: 'Insertion & activation was successfull'
                }
            }
        }

        /***
         * Activate
         */
        protected void activate()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("SELECT Members_IsActivated FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            if (dataReader.Read())
                if (dataReader["Members_IsActivated"].ToString() == "false")
                {
                    /***
                     * Activate Account
                     */
                    sqlConnection.Close();  // Close Connection
                    sqlCommand = new SqlCommand("UPDATE Members SET Members_IsActivated = 'true'", sqlConnection);    // Initialize command
                    sqlConnection.Open();   // Open Connection
                    sqlCommand.ExecuteNonQuery();

                    Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Yey! Your account has been activated successfully! You may login now...</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Login</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
                }
                else
                    Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Account has already been activated. You may login now...</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Login</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
            else
                Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Activation Section</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div style=\"background-color: white; text-align: center; padding: 20px;\"><span style=\"font-weight: bold\">Link not found (Err 404) - Probably expired. Request new link from Here:</span><a href=\"http://messenger.keivanipchihagh.ir/\" style=\"display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px\">Request New Link</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div</body></html>");
        }

        /***
         * Resend Activation
         */
        protected void resendActivation()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("SELECT Members_ActivationCode FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            if (dataReader.Read())
            {
                sendActivationMail(Request.QueryString["Email"], dataReader["Members_ActivationCode"].ToString());
                Response.Write("Code 1");
            }
            else
                Response.Write("Code 2");
        }

        /***
         * Recover Password
         */
        protected void recoverPassword()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("SELECT Members_ActivationCode FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            if (dataReader.Read())
            {
                try
                {
                    sendRecoveryMail(Request.QueryString["Email"], dataReader["Members_ActivationCode"].ToString());   // Send Mail
                }
                catch (Exception) { Response.Write("Code 0"); }                
            }
            Response.Write("Code 1");
        }

        /***
         * Request Password Recovery
         */
        protected void requestPasswordRecovery()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            if (dataReader.Read())
                Response.Write("<!--Author: Keivan Ipchi Hagh | Author URL: http://keivanipchihagh.ir/ --><!DOCTYPE HTML><html lang=\"en\"><head><title>Messenger | Reset Password</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"UTF-8\" /><link href=\"assets/img/favicon.png\" rel=\"icon\"><link rel=\"stylesheet\" href=\"css/main.css\" type=\"text/css\" media=\"all\" /><link rel=\"stylesheet\" href=\"css/style.css\" type=\"text/css\" media=\"all\" /><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><script src=\"assets/js/main.js\"></script></head><body><div class=\"main-bg container-login100\" style=\"padding-bottom: 0px\"><div class=\"sub-main-w3 wrap-login100\"><div style=\"width: 100%; padding: 1.5em\"><h1 style=\"padding: 0px\"><img src=\"assets/img/favicon.png\" style=\"width: 60px; height: 60px; vertical-align: middle; margin: 10px\" /><span>Messenger</span></h1></div><div id=\"content\" style=\"background-color: white; text-align: center; padding: 20px\"><input id=\"identifier\" name=\"identifier\" type=\"hidden\" value=\"login\" /><h3 class=\"legend\" style=\"font-weight: bold\">~ Reset Password ~</h3><div id=\"alertBox\"></div><input id=\"email\" name=\"email\" type=\"hidden\" value=\"" + Request.QueryString["Email"] + "\" /><input id=\"code\" name=\"code\" type=\"hidden\" value=\"" + Request.QueryString["ActivationCode"] + "\" /><div class=\"input\"><span class=\"fa fa-key\" aria-hidden=\"true\"></span><input type=\"password\" placeholder=\"New Password\" name=\"pass\" id=\"pass\" required=\"required\" maxlength=\"50\" /><i class=\"fa fa-eye\" aria-hidden=\"true\" title=\"Show/Hide Password\" onclick=\"this.classList.toggle('fa-eye-slash'); changePassVisual()\"></i></div><div class=\"input\"><span class=\"fa fa-key\" aria-hidden=\"true\"></span><input type=\"password\" placeholder=\"Confirm New Password\" name=\"confirm\" id=\"confirm\" required=\"required\" maxlength=\"50\" /></div><button type=\"submit\" class=\"btn submit\" onclick=\"resetPassword()\">Reset</button><a href=\"http://messenger.keivanipchihagh.ir/\" class=\"bottom-text-w3ls\" style=\"margin-top: 22px; cursor: pointer\">Remember Your Password?</a></div></div><footer style=\"position: fixed; bottom: 0;\"><div class=\"copyright\"><h2>&copy; 2020 Messenger. All rights reserved | Design by<a href=\"http://keivanipchihagh.ir/\" target=\"_blank\" style=\"text-decoration: underline\">Keivan Ipchi Hagh</a></h2></div></footer></div></body></html>");
            else
                Response.Write("<!DOCTYPE html><html lang=\"en\"><head><title id=\"title\">Messenger | Reset Password</title><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link href=\"assets/img/favicon.png\" rel=\"icon\"><link href=\"assets/img/apple-touch-icon.png\" rel=\"apple-touch-icon\"><script src=\"assets/js/main.js\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/util.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\"></head><body><div id=\"mainContainer\" class=\"limiter\"><div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><input name=\"identifier\" id=\"identifier\" type=\"hidden\" value=\"login\" /><span class=\"login100-form-title p-b-26\" style=\"text-align: left\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3>Messenger</h3> <p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><div id=\"alertBox\"></div><div style=\"width: 100%; text-align: center; font-family: 'Raleway'; margin: 5px\"><h3>Password Recovery</h3><p>This link contains Invalid signeratures. Session has been terminated due to EA violations.</p></div></div></div></div></body></html>");
        }

        /***
         * Reset Password
         */
        protected void resetPassword()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("UPDATE Members SET Members_Password = @password WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
            sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
            sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
            sqlConnection.Open();   // Open Connection
            int status = sqlCommand.ExecuteNonQuery();
            if (status >= 1)
                Response.Write("Code 1");
            else
                Response.Write("Code 0");
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
                {
                    return addresses[0];
                }
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
    }
};