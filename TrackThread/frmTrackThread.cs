using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace TrackThread
{
    public partial class frmTrackThread : Form
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(string fileName, uint access, uint share,
            IntPtr security, uint disposition, uint flags, IntPtr template);

        private static bool consoleOpened = false;

        public frmTrackThread()
        {
            InitializeComponent();
        }

        private static void OpenConsole()
        {
            if (consoleOpened) return;

            AllocConsole();
            IntPtr handle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
            FileStream stream = new FileStream(new SafeFileHandle(handle, true), FileAccess.Write);
            Console.SetOut(new StreamWriter(stream) { AutoFlush = true });
            consoleOpened = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            OpenConsole();

            btnRun.Enabled = false;
            lblStatus.Text = "-Thread Starts -";
            Console.WriteLine("-Thread Starts-");

            Thread threadA = new Thread(MyThreadClass.Thread1);
            Thread threadB = new Thread(MyThreadClass.Thread2);
            Thread threadC = new Thread(MyThreadClass.Thread1);
            Thread threadD = new Thread(MyThreadClass.Thread2);

            threadA.Name = "Thread A";
            threadB.Name = "Thread B";
            threadC.Name = "Thread C";
            threadD.Name = "Thread D";

            threadA.Priority = ThreadPriority.Highest;
            threadB.Priority = ThreadPriority.Normal;
            threadC.Priority = ThreadPriority.AboveNormal;
            threadD.Priority = ThreadPriority.BelowNormal;

            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();

            Thread waiter = new Thread(() =>
            {
                threadA.Join();
                threadB.Join();
                threadC.Join();
                threadD.Join();

                Console.WriteLine("-End of Thread-");
                BeginInvoke((MethodInvoker)(() =>
                {
                    lblStatus.Text = "-End of Thread-";
                    btnRun.Enabled = true;
                }));
            });
            waiter.IsBackground = true;
            waiter.Start();
        }
    }
}
