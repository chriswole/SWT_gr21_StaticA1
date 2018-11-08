using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Interfaces;

namespace ATM_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Position pos1=new Position(12341234, 124032421);
            Position pos2 = new Position(12341232, 124033221);
            Track tack=new Track("Kim",pos1,50000,01243,0234,"mm.dd.yyyy");
            Track tack2 = new Track("Kim", pos1, 50000, 01243, 0234, "mm.dd.yyyy");
            Danger dan= new Danger(tack,tack2,20000);
            Logger Log=new Logger();

            Log.AddToLog(dan);





        }
    }
}
