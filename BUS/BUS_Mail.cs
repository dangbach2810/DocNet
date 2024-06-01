using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using DAL;


namespace BUS
{
    public class BUS_Mail
    {
        private QLSanPhamDataContext db = new QLSanPhamDataContext();
        private string senderEmail;

        public BUS_Mail(string senderEmail)
        {
            this.senderEmail = senderEmail;
        }
       
        private string Encrytion(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        private async Task<bool> SendMailGoogleSmtp(string _from, string _to, string _subject,                                                    string _body, string _gmailsend, string _gmailpassword)
        {

            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);

            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
        private async void FormEmail(string _emailUser, string newPassword)
        {
            StringBuilder content = new StringBuilder("");
            content.Append("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Email Content</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            margin: 0;\r\n            padding: 0;\r\n            background-color: #f4f4f4;\r\n            border: 2px solid #ccc;\r\n            border-radius: 10px;\r\n            max-width: 600px;\r\n            margin: auto;\r\n            padding: 20px;\r\n        }\r\n        h2 {\r\n            text-align: center;\r\n            color: #333;\r\n            border: 2px black solid;\r\n        }\r\n        p {\r\n\r\n            margin: 10px 0;\r\n        }\r\n        p strong {\r\n            font-weight: bold;\r\n            color: #007bff;\r\n        }\r\n    </style>\r\n</head>");//header
            content.Append("<body>\r\n    <h2></h2>");//Title
            content.Append("<p><strong>Mật khẩu mới:</strong> " + newPassword + "</p>");//Doing Task
            content.Append("<i>Cảm ơn bạn đã sử dụng hệ thống.Chúng tôi sẽ tiếp tục phát triển và cải thiện hệ thống để cung cấp những giải pháp\r\n        tốt nhất cho cộng đồng người dùng.\r\n        Nếu có bất kỳ thắc mắc hoặc góp ý nào, xin đừng ngần ngại liên hệ với tôi.\r\n    </i>");//Thanks
            SendMailGoogleSmtp("locdangbach@gmail.com", _emailUser, "Hệ thống quản lý sản phẩm trong siêu thị mini", content.ToString(), "locdangbach@gmail.com", "oguh rpmo inqr adkg");
        }
        public bool Send(string email)
        {
            try
            {
                string newPassword = GetRandomPassword();
                tblEmployee tblEmployee = db.tblEmployees.Where(emp => emp.Email.Equals(email)).FirstOrDefault();
                tblEmployee.Password = Encrytion(newPassword);
                db.SubmitChanges();
                FormEmail(email, newPassword);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }       
        public string GetRandomPassword()
        {
            Random r = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(r.Next(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random r = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                return builder.ToString().ToUpper();
            }
            else return builder.ToString().ToLower();
        }
    }
}