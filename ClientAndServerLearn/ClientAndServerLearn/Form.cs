using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ClientAndServerLearn
{
    public partial class Form1
    {
        public delegate void SetTextCallback(string text,Color bkColor);
        private void SetText(String str,Color bkColor)
        {
            if(this.label1.InvokeRequired)
            {
                while(!this.label1.IsHandleCreated)
                {
                    if (this.label1.Disposing || this.label1.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(SetText);
                this.label1.Invoke(d, new object[] { str, bkColor});
                //this.BeginInvoke();
            }
            else
            {
                this.label1.Text = str;
                this.label1.BackColor = bkColor;
            }
        }
    }
}
