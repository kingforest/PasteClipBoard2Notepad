using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PasteClipBoard2Notepad
{
    public partial class Service : ServiceBase
    {
        KeyboardHook k_hook;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下
            k_hook.Start();//安装键盘钩子
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            //UnregisterHotKey(this.ServiceHandle, 100);
            k_hook.Stop();
            base.OnStop();
        }

        protected override void OnPause()
        {
            //UnregisterHotKey(this.ServiceHandle, 100);
            k_hook.Stop();
            base.OnPause();
        }

        protected override void OnContinue()
        {
            k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下
            k_hook.Start();//安装键盘钩子
            base.OnContinue();
        }

        protected override void OnShutdown()
        {
            //UnregisterHotKey(this.ServiceHandle, 100);
            k_hook.Stop();
            base.OnShutdown();
        }

        //3.判断输入键值（实现KeyDown事件）
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            //判断按下的键（Alt + A）
            if (e.KeyValue == (int)Keys.Z && (int)Control.ModifierKeys == (int)Keys.LControlKey && (int)Control.ModifierKeys == (int)Keys.LWin)
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                Proc.StartInfo.FileName = "notepad.exe";
                Proc.StartInfo.UseShellExecute = false;
                Proc.StartInfo.RedirectStandardInput = true;
                Proc.StartInfo.RedirectStandardOutput = true;
                Proc.Start();
                while (Proc.MainWindowHandle == IntPtr.Zero)
                {
                    Proc.Refresh();
                }
                IntPtr vHandle = FindWindowEx(Proc.MainWindowHandle, IntPtr.Zero, "Edit", null);
                // 传递数据给记事本 
                SendMessage(vHandle, WM_SETTEXT, 0, Clipboard.GetText(TextDataFormat.Text));
            }
        }

        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        public const uint WM_SETTEXT = 0x000C;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
    }
}
