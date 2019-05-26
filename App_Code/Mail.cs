using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text;

/// <summary>
/// SendMail
/// </summary>
[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Mail : System.Web.Services.WebService {
    string myEmail = ConfigurationManager.AppSettings["myEmail"];
    string myPassword = ConfigurationManager.AppSettings["myPassword"];
    int myServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["myServerPort"]);
    string myServerHost = ConfigurationManager.AppSettings["myServerHost"];
    string myEmail_cc = ConfigurationManager.AppSettings["myEmail_cc"];

    public Mail() {
    }

    public bool SendMail(string sendTo, string subject, string body, string file, bool send_cc) {
        try {
            string footer = @"
<br>
<br>
<br>
<div>
    <img alt=""rivierasplit.com"" height=""40"" src=""https://www.rivierasplit.com/img/logo.svg"" style=""float:left"" width=""190"" />
</div>
<br>
<br>
<br>
<div style=""color:gray"">
    RIVIERA SPLIT<br>
    <a href=""mailto:info@rivierasplit.com?subject=Upit"">info@rivierasplit.com</a><br>
    <a href=""https://www.rivierasplit.com"">www.rivierasplit.com</a>
</div>";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(myEmail);
            mail.To.Add(sendTo);
            if (send_cc) {
                mail.CC.Add(myEmail_cc);
            }
            mail.Subject = subject;
            mail.Body = string.Format(@"
{0}

{1}", body, footer);
            mail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(file)) {
                Attachment attachment = new Attachment(Server.MapPath(file.Replace("../", "~/")));
                mail.Attachments.Add(attachment);
            }
            SmtpClient smtp = new SmtpClient(myServerHost, myServerPort);
            NetworkCredential Credentials = new NetworkCredential(myEmail, myPassword);
            smtp.Credentials = Credentials;
            smtp.Send(mail);
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    //[WebMethod]
    //public string SendMail(string sendTo, string subject, string body, string[] cc) {
    //    try {
    //        MailMessage mail = new MailMessage();
    //        mail.From = new MailAddress(myEmail);
    //        mail.To.Add(sendTo);
    //        if (cc.Length > 0) {
    //            foreach (string c in cc) {
    //                mail.CC.Add(c);
    //            }
    //        }
    //        mail.Subject = subject;
    //        mail.Body = body;
    //        mail.IsBodyHtml = true;
    //        SmtpClient smtp = new SmtpClient(myServerHost, myServerPort);
    //        NetworkCredential Credentials = new NetworkCredential(myEmail, myPassword);
    //        smtp.Credentials = Credentials;
    //        smtp.Send(mail);
    //        return "message sent successfully";
    //    } catch (Exception e) {
    //        return e.Message;
    //    }
    //}



}
