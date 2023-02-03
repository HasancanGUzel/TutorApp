using OzelDers.Web.EmailServices.Abstract;
using System.Net.Mail;
using System.Net;

namespace OzelDers.Web.EmailServices.Concrete
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _userName;
        private string _password;

        public SmtpEmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            //program.cs deki 67 satırda bilgileri buraya gönderiyoruz.
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _userName = userName;
            _password = password;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //client kısmı kimden gidiyor
            var client = new SmtpClient(_host, _port)  // dışarıdan gelen host ve port bilgisini client atıyoruz
            {
                Credentials = new NetworkCredential(_userName, _password), // kullanıcı adı ve şifre veriyoruz
                EnableSsl = _enableSSL // true yada false bilgiini veriyoruz
            };
            //burası ise kime gidecek
            return client.SendMailAsync( // mail yollama işlemi burada yapılıyor
                new MailMessage(_userName, email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                });
        }
    }
}
