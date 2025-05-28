using System.Net.Mail;
using System.Security.Cryptography;

namespace Dotel2.Service
{
    public class SendMail
    {
        public string GenerateVerificationCode()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-", "");
            }
        }
        public void SendEmailVerification(string email, string verificationCode)
        {
            try
            {
                var fromAddress = new MailAddress("dotelhouse@outlook.com", "Dotel");
                var toAddress = new MailAddress(email);
                const string fromPassword = "dotel2024";
                const string subject = "Email Verification";
                string body = $"Your verification code is {verificationCode}";

                var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
        public void SendSMSVerification(string phoneNumber, string verificationCode)
        {
/*            SpeedSMSAPI api = new SpeedSMSAPI("J2UN35TT7vpudKKlUu_WT6DSawofkz1G");

            String[] phones = new String[] { "849xxxxxxx" };
            String str = "Nội dung sms";
            String response = api.sendSMS(phones, str, 2, "");
            //String response = api.sendMMS(phones, str, "https://", "device ID");
            Console.WriteLine(response);
            Console.ReadLine();*/
        }
    }
}
