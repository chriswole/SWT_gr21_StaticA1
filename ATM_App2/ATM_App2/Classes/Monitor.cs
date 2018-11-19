using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Events;

namespace ATM_App2.Classes
{
    public class Monitor 
    {
        //Constructor
        public Monitor(string test_)
        {

        }

        public void InitMonitor()
        {
            List<Monitor> airspaceMonitored = new List<Monitor>();

            if (airspaceMonitored != null)
            {
                airspaceMonitored.ForEach(Console.WriteLine);
            }

            else
            {
                Console.WriteLine("There are currently no planes in the airspace\n");
            }

            Console.WriteLine(true);

        }

        public void UpdateMonitor()
        {

            //Subscribing to planes entering the airspace
            InOutTrack entering = new InOutTrack();
            entering.listInUpdated += new System.EventHandler<EnteredTrackArgs>(OnEntered);
            
            //Subscribing to planes leaving the airspace
            InOutTrack leaving = new InOutTrack();
            leaving.listOutUpdated += new System.EventHandler<LeftTrackArgs>(OnDeleted);


            //Subscribing to Separation events
            Separation monitoring = new Separation(new LogToFile());
            monitoring.DangerListUpdated += new EventHandler<DangerlistArgs>(OnNewDanger);
            

        }

        
        private void OnEntered(object sender, EnteredTrackArgs e)
        {
            //Listing planes entering airspace
            Console.WriteLine("{0} has entered til airspace", e.listEntered);
        }

        private void OnDeleted(object sender, LeftTrackArgs e)
        {
            //Listing planes leaving airspace
            Console.WriteLine("{0} has left the airspace", e.listLeft);
        }

        private void OnNewDanger(object sender, DangerlistArgs e)
        {
            //Listing available dangers
            Console.WriteLine("{0} are too close!", e.DangerList_);
        }
    }
}
