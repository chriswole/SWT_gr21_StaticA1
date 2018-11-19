using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ATM_App2.Classes;
using ATM_App2.Interfaces;


namespace ATM_App2.Events
{
    public class TimeKeeper:ITimekeeper
    {
        public EventHandler TimeElapsedIn;
        public EventHandler TimerElapsedOut;
        static System.Windows.Forms.Timer timer1 =new System.Windows.Forms.Timer();

        
        public void startTimerIn()
        {
            timer1.Interval = 5000; // 5 second
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(TimeElapsedIn);
        }
        public void startTimerOut()
        {
            timer1.Interval = 5000; // 5 second
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(TimerElapsedOut);
        }

        public void stopTimer()
        {
            timer1.Enabled = false;
        }
        
    }
}


