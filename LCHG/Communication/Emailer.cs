using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LCHG.Communication
{
    // general emailer class.
    public class Emailer
    {
        private const string USER_NAME = "LOWCOSTBEDS\\SQLAdmin";
        private const string PASSWORD = "SQL4dmin123";
        private const string HOST_NAME = "svrexch1.lowcostbeds.com";
        private const string FROM_NAME = "PPCEngine@lowcostholidays.com";

        private const int PORT = 587;

        public static void SendEmail(string toEmail, string subject, string message)
        {
            MailMessage mail = new MailMessage(FROM_NAME, toEmail, subject, message);
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(USER_NAME, PASSWORD);
            client.Port = PORT;
            client.Host = HOST_NAME;
            client.EnableSsl = false;
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(mail);
            }
            catch (Exception)
            {
            }
        }

        public static void SendEmail(string toEmail, string subject, string function, Exception exception)
        {
            string error = "Error occured in : " + function + " : " + Environment.NewLine + Environment.NewLine;
            error += CreateExceptionMessage(exception);
            SendEmail(toEmail, subject, error);
        }

        private static void SendEmail(string toEmail, string subject, Exception exception)
        {
            SendEmail(toEmail, subject, CreateExceptionMessage(exception));
        }

        private static string CreateExceptionMessage(Exception exception)
        {
            string exceptionMessage;

            exceptionMessage = exception.Message + Environment.NewLine + Environment.NewLine;
            exceptionMessage += exception.StackTrace + Environment.NewLine + Environment.NewLine;

            if (exception.InnerException != null)
            {
                exceptionMessage = "InnerException" + Environment.NewLine + Environment.NewLine;
                exceptionMessage += exception.InnerException.Message + Environment.NewLine + Environment.NewLine;
                exceptionMessage += exception.InnerException.StackTrace + Environment.NewLine + Environment.NewLine;
            }
            return exceptionMessage;
        }
    }
}
