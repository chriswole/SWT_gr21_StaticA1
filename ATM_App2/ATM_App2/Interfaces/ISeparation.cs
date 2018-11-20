using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2.Interfaces
{
    public interface ISeparation
    {
        event EventHandler<DangerlistArgs> DangerListUpdated;
        void CalculateDistances(List<Track> trackList);
 
    }
}