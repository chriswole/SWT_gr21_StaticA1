using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface ISeparation
    {
        List<Danger> newDangers_ { get; set; }
        List<Danger> OldDangers_ { get; set; }

        void calculateDistances(List<Track> trackList);
        void raiseAlarm();
        void deactivateAlarm();
    }
}
