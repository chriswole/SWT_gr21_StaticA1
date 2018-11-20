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
using ATM_App2.Interfaces;

namespace ATM_App2.Classes
{
    public class Monitor 
    {
        //Constructor
        public Monitor()
        {

        }

        public void InitMonitor()
        {
            List<Monitor> airspaceMonitored = new List<Monitor>();
            List<Monitor> updatedAirspaceMonitored = new List<Monitor>();

            if (airspaceMonitored.Count==0)
            {
                Console.WriteLine("There are currently no planes in the airspace");
            }

            else
            {
                //Adds the airspaceMonitored list to the updatedAirspaceMonitored list
                updatedAirspaceMonitored.AddRange(airspaceMonitored);
                //Clears airspaceMonitored list
                airspaceMonitored.Clear();
                //Writes out the planes in the airspace monitored in console
                updatedAirspaceMonitored.ForEach(Console.WriteLine);

            }
            

        }

        public void UpdateMonitor(ISeperation monitoring)
        {

            //Subscribing to planes entering the airspace
            InOutTrack entering = new InOutTrack();
            entering.listInUpdated += new System.EventHandler<EnteredTrackArgs>(OnEntered);
            
            //Subscribing to planes leaving the airspace
            InOutTrack leaving = new InOutTrack();
            leaving.listOutUpdated += new System.EventHandler<LeftTrackArgs>(OnDeleted);


            //Subscribing to Separation events
            monitoring.DangerListUpdated += new EventHandler<DangerlistArgs>(OnNewDanger);
            

        }

        
        private void OnEntered(object sender, EnteredTrackArgs e) 
        {
            List<Monitor> enterList = new List<Monitor>();
            //Listing planes entering airspace
            Console.WriteLine("{0} has entered til airspace", e.listEntered);
            //enterList.Add(listEntered);
            //airspaceMonitored.AddRange(enterList);
            //enterList.Clear();

        }

        private void OnDeleted(object sender, LeftTrackArgs e)
        {
            List<Monitor> leavingList = new List<Monitor>();
            //Listing planes leaving airspace
            Console.WriteLine("{0} has left the airspace", e.listLeft);
            //leavingList.Add(listLeft);
            //airspaceMonitored.AddRange(leavingList);
            //leavingList.Clear();
        }

        private void OnNewDanger(object sender, DangerlistArgs e)
        {
            //Listing active dangers
            Console.WriteLine("{0} are too close!", e.DangerList_);
        }
    }
}
