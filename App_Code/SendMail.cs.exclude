using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using IGPROG;

/// <summary>
/// Summary description for SendMail
/// </summary>
/// 
namespace IGPROG {
    public class SendMail {
        string _InfoMessage;
        private bool _IsSuccess;

        public SendMail() {
            _InfoMessage = null;
            _IsSuccess = false;
        }

        public bool IsSuccess {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }


        public void Send(string sendToEmail, string messageSubject, string messageBody, string successMessage, string errorMessage, List<string> attachments) {
            ApplicationSettings applicationSettings = new ApplicationSettings();
       
            string myEmail = applicationSettings.Email;
            string myPassword = applicationSettings.EmailPassword;
            int myServerPort = Convert.ToInt32(applicationSettings.ServerPort);
            string myServerHost = applicationSettings.ServerHost;
            string additionalMail1 = applicationSettings.AdditionalMail1;
            string additionalMail2 = applicationSettings.AdditionalMail2;
            string additionalMail3 = applicationSettings.AdditionalMail3;

            try {
                MailMessage mailMessage = new MailMessage();

                SmtpClient Smtp_Server = new SmtpClient();
                Smtp_Server.UseDefaultCredentials = false;
                Smtp_Server.Credentials = new NetworkCredential(myEmail, myPassword);
                Smtp_Server.Port = myServerPort;
                Smtp_Server.EnableSsl = true;
                Smtp_Server.Host = myServerHost;

                mailMessage.To.Add(sendToEmail);    //Mail na koji se salje poruka
                
                //Dodatni mail na koji se salje poruka ako se mail šalje meni
                if (myEmail == sendToEmail) {
                    if (additionalMail1 != null){
                        mailMessage.To.Add(additionalMail1);
                    }
                    if (additionalMail2 != null) {
                        mailMessage.To.Add(additionalMail2);
                    }
                    if (additionalMail3 != null) {
                        mailMessage.To.Add(additionalMail3);
                    }
                }

                mailMessage.From = new MailAddress(myEmail);   //Moj mail sa koje se salje poruka
                mailMessage.Subject = messageSubject;
                mailMessage.Body = messageBody;

                for (int i = 0; i < attachments.Count(); i++) {
                    Attachment at = new Attachment(attachments[i]);
                    mailMessage.Attachments.Add(at);
                }

                Smtp_Server.Send(mailMessage);
                _InfoMessage = successMessage;
                _IsSuccess = true;

            }
            catch (Exception ex) {
                _InfoMessage = errorMessage;
                _IsSuccess = false;
                return;
            }
        }


//        private string SuccessMessage()
//        {
        
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine(string.Format(@"
//            <div style=""border-style:dotted; border-width:1px"">
//                <h3>Message Send</h3> 
//                <h4>Details:</h4>
//            </div>"));

//            return sb.ToString();
//        }

//        private string MessageToCustomer()
//        {
           

//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine(string.Format(@"Hello,"));

//            return sb.ToString();

//        }


//        private string ErrorMessage()
//        {
//            StringBuilder sb = new StringBuilder();
//            Booking booking = new Booking();

//            sb.AppendLine(string.Format(@"
//                   <div class=""alignLeft"">
//                    <h3>{0}</h3>
//                    <p>{1}</p>
//                    <p>{2}</p>
//                   </div>"
//                , "Error!"
//                , booking.Guest
//                , booking.Arrival));

//            return sb.ToString();
//        }


        public string InfoMessage {
            get { return _InfoMessage; }
        }



    }
}