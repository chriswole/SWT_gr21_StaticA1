using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2.Interfaces
{
   public interface IInAirSpace
    {
        //event handlers
        void OnTrackCreated(object sender, TrackArgs args);
    }
}
