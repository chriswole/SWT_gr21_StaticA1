using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2.Interfaces
{
    public interface ISeperation
    {
        List<Danger> newDangers_ { get; set; }
        List<Danger> oldDangers_ { get; set; }

        void calculateDistances(List<Track> tracklist);
        void raiseAlarm();
        void deactivateAlarm();
    }
}