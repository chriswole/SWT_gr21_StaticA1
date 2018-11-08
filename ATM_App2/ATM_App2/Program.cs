using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2
{
    class Program
    {
        static void Main(string[] args)
        {
            Position pos=new Position(24134, 214124);
            Track track=new Track("Hans",pos,2134,134,345,"dd.mm.yyyyy");
            LogToFile log=new LogToFile();

            log.AddEnteredTrackToLog(track);
            log.AddExitedTrackToLog(track);
            
        }
    }
}
