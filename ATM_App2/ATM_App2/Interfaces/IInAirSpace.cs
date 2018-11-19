using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2.Interfaces
{
<<<<<<< HEAD
    interface IInAirSpaceObserver
=======
   public interface IInAirSpace
>>>>>>> 21deeb8977d32b20482f9a5b75087353a3076b96
    {
        event EventHandler<AirspaceTrackArgs> AirspaceUpdated;
        event  EventHandler<TrackArgs> EnteredTrack;
        event EventHandler<TrackArgs> LeavingTrack;
    }
}
