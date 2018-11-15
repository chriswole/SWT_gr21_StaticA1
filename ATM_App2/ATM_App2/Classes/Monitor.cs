using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App2.Classes
{
    class Monitor 
    {
        //Constructor
        public Monitor()
        { }

        public void InitMonitor()
        {
            List<Monitor> airspaceMonitored = new List<Monitor>();

            if (airspaceMonitored != null)
            {
                airspaceMonitored.ForEach(Console.WriteLine);
            }

            else
            {
                Console.Write("There are currently no planes in the airspace\n");
            }

        }

        public void UpdateMonitor()
        {
            /*
            //Subscribing to inOutTrack events
            inOutTrack whenSomethingEntersOrLeavesAirspace = new inOutTrack();
            whenSomethingEntersAirspace.addPlaneToAirspace += UpdateMonitor;
            whenSomethingLeavesAirspace.removePlaneFromAirspace -=UpdateMonitor; //Unsubscribing from inOutTrack event
            
             //Subscribing to Separation
             Separation listAllActiveDangers = new Separation();
             Separation.addActiveDanger += UpdateMonitor; //Subscribing to Separation event
             Separation.removeActiveDanger -= UpdateMonitor //Unsubscribing from Separation event
             */
        }
    
    }
}
