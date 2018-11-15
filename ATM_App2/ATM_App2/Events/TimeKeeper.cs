using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ATM_App2.Classes;


namespace ATM_App2.Events
{
    public class TimeKeeper
    {
        public EventHandler TimeElapsedIn;
        static System.Windows.Forms.Timer timer1 =new System.Windows.Forms.Timer();

        
        public void startTimer()
        {
            timer1.Interval = 5000; // 5 second
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(TimeElapsedIn);
        }


        public void stopTimer()
        {
            timer1.Enabled = false;
        }
        
    }
}


