using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Events;

namespace ATM_App2.Interfaces
{
    public interface IInOutTrackHandler
    {
        event EventHandler<EnteredTrackArgs> listInUpdated;
        event EventHandler<LeftTrackArgs> listOutUpdated;

        void OnEnteredTrack(object sender, TrackArgs totrack);

        void OnLeavingTrack(object sender, TrackArgs totrack);

        void TimeElapsedIn(object sender, EventArgs e);

        void TimeElapsedOut(object sender, EventArgs e);

    }

}

