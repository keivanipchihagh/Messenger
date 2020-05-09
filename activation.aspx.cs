using System;
using System.Net;
using System.Net.Mail;

namespace Messenger
{
    public partial class activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string targetMail = Request.Form["Email"];
                MailMessage mailMessage = new MailMessage("	noreply@keivanipchihagh.ir", targetMail);
                mailMessage.Subject = "";
                mailMessage.Body = "<input type='button' style=\"width: 200px; height: 30px;\" value='howdy!' onclick='alert(\"Howdy!\")'></br >" + "<a href='http://google.com/'>GOOGLE</a></br >";                
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("webmail.keivanipchihagh.ir");
                smtpClient.Credentials = new NetworkCredential("tempMail@keivanipchihagh.ir", "76b*sp0D");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
                //para1.InnerHtml = "Mail Sent.";
            }
            catch (Exception ex)
            {
                //para1.InnerHtml = ex.Message;
            }
        }
    }
}