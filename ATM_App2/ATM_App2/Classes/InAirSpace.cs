using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App2.Classes
{
    public class AirspaceTrackArgs: EventArgs
    {
        public List<Track> TracksInAirSpace { get; set; }
    }

    public class InAirSpace
    {
        private List<Track> _tracksInAirspace;

        public EventHandler<AirspaceTrackArgs> AirspaceUpdated;


    }
}
