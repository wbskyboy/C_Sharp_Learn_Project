using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
namespace ClientAndServerLearn
{
    public partial class Form1 : Form
    {
        private Thread svrThread = null;
        private ManualResetEvent _ShutdownEvent = new ManualResetEvent(false);
        private ManualResetEvent _PauseEvent = new ManualResetEvent(true);
        public Form1()
        {
            InitializeComponent();
            ServerStart();
        }

        private void ServerStart()
        {
            svrThread = new Thread(new ThreadStart(ServerModel));         
        }
        public void ServerModel()
        {
            while (true)
            {
                _PauseEvent.WaitOne(Timeout.Infinite);
                if (_ShutdownEvent.WaitOne(0))
                    break;
                SetText("hello this is world!",Color.Red);
                Thread.Sleep(100);
                SetText("Fuck away this is world!",Color.White);
                Thread.Sleep(100);
            }
        }
        private void PauseThread()
        {
            _PauseEvent.Reset();
        }
        private void RusumeThread()
        {
            _PauseEvent.Set();
        }
        private void ExitThread()
        {
            _ShutdownEvent.Set();
            _PauseEvent.Set();
            svrThread.Join();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (svrThread.ThreadState == ThreadState.Unstarted)
            {
                svrThread.Start();
            }
            else
                RusumeThread();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PauseThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}
