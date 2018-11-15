using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace ATM_App2.Events
{
    public class TimeKeeper
    {
        private static System.Timers.Timer tmr;
        private int counter = 0;

        public TimeKeeper()
        {
            System.Timers.Timer tmr = new System.Timers.Timer();
            tmr.Interval = 5000;
            tmr.Tick += new EventHandler(tmr_Tick);
        }

        public void tmr_Tick(object sender, EventArgs e)
        {
            counter++;
        }

        private void startTimer(object sender, EventArgs e)
        {
            tmr.Start();
        }

        private void stopTimer(object sender, EventArgs e)
        {
            tmr.Stop();
        }
    }
}
