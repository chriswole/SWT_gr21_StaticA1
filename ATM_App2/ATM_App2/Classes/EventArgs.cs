using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App2.Classes
{

    
    public class TrackArgs : EventArgs  //For enter exit events
    {
        public Track newTrack_ { get; set; }

        public TrackArgs(Track track)
        {
            newTrack_ = track;
        }

        public TrackArgs(){}
    }

    public class AirspaceTrackArgs : EventArgs
    {
        public List<Track> TracksInAirSpace { get; set; }

        public AirspaceTrackArgs(List<Track> airspaceList)
        {
            TracksInAirSpace = airspaceList;
        }

        public AirspaceTrackArgs() {}
    }
}
