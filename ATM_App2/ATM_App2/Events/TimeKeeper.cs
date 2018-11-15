using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;


namespace ATM_App2.Events
{
    public class TimeKeeper
    {

        static System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        private int counter;

        private void startTimer(object sender, EventArgs e)
        {
            int counter = 0;
            timer1.Interval = 5000; // 5 second
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           timer1.Enabled = false;
           
        }

        
    }
}


