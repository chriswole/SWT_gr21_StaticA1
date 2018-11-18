using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using TransponderReceiver;

namespace ATM_App2.Interfaces
{
    public interface ITrackFactory
    {
        event EventHandler<TrackArgs> TrackCreated;
    }
}
