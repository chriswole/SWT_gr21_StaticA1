using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Events;
using ATM_App2.Interfaces;
using TransponderReceiver;

namespace ATM_App2
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
            Position pos=new Position(24134, 214124);
            Track track=new Track("Hans",pos,2134,134,345,"dd.mm.yyyyy");
            LogToFile log=new LogToFile(); */

            ITransponderReceiver receiver = TransponderReceiver.TransponderReceiverFactory.CreateTransponderDataReceiver();
            ITrackFactory trackFactory = new TrackFactory(receiver, new StringParser());
            ITrackOpticsProvider optics = new TrackOpticsProvider();
            IInAirSpaceObserver airspaceObserver = new InAirSpaceObserver(optics);
            ILogToFile filelogger = new LogToFile();
            ISeparation seperationObserver = new Separation(filelogger);
            IInOutTrackHandler inOutTrackHandler = new InOutTrackHandler();

            trackFactory.TrackCreated += airspaceObserver.OnTrackCreated;
            airspaceObserver.EnteredTrack += inOutTrackHandler.OnEnteredTrack;
            airspaceObserver.AirspaceUpdated += seperationObserver.OnAirspaceUpdated;
            airspaceObserver.LeavingTrack += inOutTrackHandler.OnLeavingTrack;

            /*Tænkte dette vil virke, jeg ved ikke om andre klasser end egne er rigtigt initialiseret korrekt.
            - Koden start og afslutter, jeg ved ikke om der skal være et vile true loop og om dette så kan være tomt...


            Har fosøgt at debugge udmiddelbart tror jeg det er fordi jeg har instansieret jeres klasser forkert.

            Overvej at lave et console output ved logevents??
            
            */

            System.Console.WriteLine("Logging events to file...");

            while (true)
            {
                // need some kind of escape clause
            }


        }
    }
}
