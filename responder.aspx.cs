using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Messenger
{
    public partial class responder : System.Web.UI.Page
    {
        //public const string connectionString = "Data Source = .; Initial Catalog = Messenger; Integrated Security = True";
        public const string connectionString = "Data Source=www.keivanipchihagh.ir;Initial Catalog = keivani3_Messenger; Persist Security Info=True;User ID = keivani3_keivan; Password=Keivan25251380";
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);    // Initialize connection

                switch (Request.QueryString["Action"])
                {
                    /*** ---------------------------------------------------------------------------------------------------------------------------------------------------------------- */
                    case "signup":

                        /***
                         * Check If UserName Is Taken
                         */
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
                                sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
                                sqlCommand.Parameters.Add(new SqlParameter("@activationCode", activationCode));  // Add parameter
                                sqlConnection.Open();   // Open Connection
                                sqlCommand.ExecuteNonQuery();   // Insert
                                sqlConnection.Close();  // Close Connection

                                Response.Write("Code 1");   // Code 1: 'Insertion & activation was successfull'
                            }
                        }
                        break;

                    /*** ---------------------------------------------------------------------------------------------------------------------------------------------------------------- */
                    case "login":
                        sqlCommand = new SqlCommand("SELECT Members_IsActivated FROM Members WHERE Members_Email = @email AND Members_Password = @password", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                        sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
                        sqlConnection.Open();   // Open connection

                        dataReader = sqlCommand.ExecuteReader();
                        if (dataReader.Read())
                            if (dataReader["Members_IsActivated"].ToString() == "true")
                                Response.Write("Code 1");   // Code 1: 'User exists'
                            else
                                Response.Write("Code 2");   // Code 1: 'Account has not been activated yet'
                        else
                            Response.Write("Code 0");   //  Code 0: 'User does not exist'

                        break;

                    /*** ---------------------------------------------------------------------------------------------------------------------------------------------------------------- */
                    case "activate":
                        sqlCommand = new SqlCommand("SELECT Members_IsActivated FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                        sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
                        sqlConnection.Open();   // Open Connection
                        dataReader = sqlCommand.ExecuteReader();

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

                                Response.Write("<!DOCTYPE html><html lang=\"en\"><head><title id=\"title\">Messenger | Activated</title><meta charset=\"UTF - 8\"><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1\"><link href=\"assets / img / favicon.png\" rel=\"icon\"><link href=\"assets / img / apple - touch - icon.png\" rel=\"apple - touch - icon\"><script src=\"assets / js / main.js\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/util.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\"></head><body><div id=\"mainContainer\" class=\"limiter\"><div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3><p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><span id=\"dialog\" style=\"color: limegreen; text-decoration: underline\"></span><div style=\"text-align: center\"><h4 style=\"margin: 10px; font-family: 'Raleway'\">Account Activated Successfully!</h4><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" style=\"font-family: 'Raleway'\" onclick=\"window.location.replace('default.aspx')\">LOGIN</button></div></div></div></div></div></div</body></html>");
                            }
                            else
                                Response.Write("<!DOCTYPE html><html lang=\"en\"><head><title id=\"title\">Messenger | Expired</title><meta charset=\"UTF - 8\"><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1\"><link href=\"assets / img / favicon.png\" rel=\"icon\"><link href=\"assets / img / apple - touch - icon.png\" rel=\"apple - touch - icon\"><script src=\"assets / js / main.js\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/util.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\"></head><body><div id=\"mainContainer\" class=\"limiter\"><div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3><p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><span id=\"dialog\" style=\"color: limegreen; text-decoration: underline\"></span><div style=\"text-align: center\"><h4 style=\"margin: 10px; font-family: 'Raleway'\">Expired Link!</h4><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" style=\"font-family: 'Raleway'\" onclick=\"window.location.replace('default.aspx')\">LOGIN</button></div></div></div></div></div></div</body></html>");
                        else
                            Response.Write("<!DOCTYPE html><html lang=\"en\"><head><title id=\"title\">Messenger | Not Found - 404</title><meta charset=\"UTF - 8\"><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1\"><link href=\"assets / img / favicon.png\" rel=\"icon\"><link href=\"assets / img / apple - touch - icon.png\" rel=\"apple - touch - icon\"><script src=\"assets / js / main.js\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/util.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\"></head><body><div id=\"mainContainer\" class=\"limiter\"><div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3><p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><span id=\"dialog\" style=\"color: limegreen; text-decoration: underline\"></span><div style=\"text-align: center\"><h4 style=\"margin: 10px; font-family: 'Raleway'\">Error 404 - Invalid Link!</h4><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" style=\"font-family: 'Raleway'\" onclick=\"window.location.replace('default.aspx')\">LOGIN</button></div></div></div></div></div></div</body></html>");
                        break;

                    case "resendActivationCode":
                        sqlCommand = new SqlCommand("SELECT Members_ActivationCode FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                        sqlConnection.Open();   // Open Connection
                        dataReader = sqlCommand.ExecuteReader();

                        if (dataReader.Read())
                        {
                            sendActivationMail(Request.QueryString["Email"], dataReader["Members_ActivationCode"].ToString());
                            Response.Write("Code 1");
                        }
                        else
                            Response.Write("Code 2");
                        break;

                    case "recoverPassword":
                        sqlCommand = new SqlCommand("SELECT Members_ActivationCode FROM Members WHERE Members_Email = @email", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                        sqlConnection.Open();   // Open Connection
                        dataReader = sqlCommand.ExecuteReader();

                        if (dataReader.Read())
                        {
                            sendRecoverynMail(Request.QueryString["Email"], dataReader["Members_ActivationCode"].ToString());   // Send Mail
                            Response.Write("Code 1");
                        }
                        else
                            Response.Write("Code 2");
                        break;

                    case "requestPasswordRecovery":
                        sqlCommand = new SqlCommand("SELECT Members_ID FROM Members WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                        sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
                        sqlConnection.Open();   // Open Connection
                        dataReader = sqlCommand.ExecuteReader();

                        if (dataReader.Read())
                            Response.Write("<!DOCTYPE html><html lang=\"en\"><head><title id=\"title\">Messenger | Reset Password</title><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link href=\"assets/img/favicon.png\" rel=\"icon\"><link href=\"assets/img/apple-touch-icon.png\" rel=\"apple-touch-icon\"><script src=\"assets/js/main.js\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><!-- StyleSheetes --><link rel=\"stylesheet\" type=\"text/css\" href=\"css/util.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\"></head><body><div id=\"mainContainer\" class=\"limiter\"><div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><input name=\"identifier\" id=\"identifier\" type=\"hidden\" value=\"login\" /><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3><p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><div id=\"alertBox\"></div><div style=\"width: 100%; text-align: center; font-family: 'Raleway'; margin: 5px\"><h3>Password Recovery</h3></div><input id=\"email\" name=\"email\" type=\"hidden\" value=\"" + Request.QueryString["Email"] + "\" /><input id=\"code\" name=\"code\" type=\"hidden\" value=\"" + Request.QueryString["ActivationCode"] + "\" /><div class=\"wrap-input100 validate-input\"><span class=\"btn-show-pass\"><i class=\"fa fa-eye\" aria-hidden=\"true\" onclick=\"this.classList.toggle('fa-eye-slash'); changePassVisual()\"></i></span><input class=\"input100\" type=\"password\" name=\"pass\" id=\"pass\" placeholder=\"New Password\" required=\"required\"></div><div class=\"wrap-input100 validate-input\"><span class=\"btn-show-pass\"></span><input class=\"input100\" type=\"password\" name=\"confirm\" id=\"confirm\" placeholder=\"Confirm New Password\" required=\"required\"></div><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" onclick=\"resetPassword()\">Reset Password</button></div></div></div></div></div></body></html>");
                        else
                            Response.Write("<!DOCTYPE html><html lang=\"en\"><head><title id=\"title\">Messenger | Reset Password</title><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link href=\"assets/img/favicon.png\" rel=\"icon\"><link href=\"assets/img/apple-touch-icon.png\" rel=\"apple-touch-icon\"><script src=\"assets/js/main.js\"></script><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css\"><link href=\"https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i\" rel=\"stylesheet\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/util.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"css/main.css\"></head><body><div id=\"mainContainer\" class=\"limiter\"><div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><input name=\"identifier\" id=\"identifier\" type=\"hidden\" value=\"login\" /><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3> <p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><div id=\"alertBox\"></div><div style=\"width: 100%; text-align: center; font-family: 'Raleway'; margin: 5px\"><h3>Password Recovery</h3><p>This link contains Invalid signeratures. Session has been terminated due to EA violations.</p></div></div></div></div></body></html>");

                        break;

                    case "resetPassword":
                        sqlCommand = new SqlCommand("UPDATE Members SET Members_Password = @password WHERE Members_Email = @email AND Members_ActivationCode = @activationCode", sqlConnection);    // Initialize command
                        sqlCommand.Parameters.Add(new SqlParameter("@password", Request.QueryString["Password"]));  // Add parameter
                        sqlCommand.Parameters.Add(new SqlParameter("@email", Request.QueryString["Email"]));  // Add parameter
                        sqlCommand.Parameters.Add(new SqlParameter("@activationCode", Request.QueryString["ActivationCode"]));  // Add parameter
                        sqlConnection.Open();   // Open Connection
                        sqlCommand.ExecuteNonQuery();
                        Response.Write("Code 1");
                        break;
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
        protected void sendRecoverynMail(string emailAddress, string activationCode)
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
    }
};