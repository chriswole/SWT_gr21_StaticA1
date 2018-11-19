using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2.Interfaces
{
    public interface IInAirSpaceObserver
    {
        event EventHandler<AirspaceTrackArgs> AirspaceUpdated;
        event  EventHandler<TrackArgs> EnteredTrack;
        event EventHandler<TrackArgs> LeavingTrack;
    }
}
