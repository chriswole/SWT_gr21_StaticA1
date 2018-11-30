using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
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
            ISeparation separationObserver = new Separation(filelogger);
            IInOutTrackHandler inOutTrackHandler = new InOutTrackHandler(filelogger);
            Monitor monitorHandler = new Monitor();

            // InAirspace
            trackFactory.TrackCreated += airspaceObserver.OnTrackCreated;
            // InOutTrackHandler
            airspaceObserver.EnteredTrack += inOutTrackHandler.OnEnteredTrack;
            airspaceObserver.LeavingTrack += inOutTrackHandler.OnLeavingTrack;
            // Separation
            airspaceObserver.AirspaceUpdated += separationObserver.OnAirspaceUpdated;
            // Monitor 
            separationObserver.DangerListUpdated += monitorHandler.OnDangerListUpdated;
            inOutTrackHandler.listInUpdated += monitorHandler.OnListInUpdated;
            inOutTrackHandler.listOutUpdated += monitorHandler.OnListOutUpdated;
            airspaceObserver.AirspaceUpdated += monitorHandler.OnAirspaceUpdated;
            
            monitorHandler.UpdateMonitor();
            





            /*Tænkte dette vil virke, jeg ved ikke om andre klasser end egne er rigtigt initialiseret korrekt.
            - Koden start og afslutter, jeg ved ikke om der skal være et vile true loop og om dette så kan være tomt...


            Har fosøgt at debugge udmiddelbart tror jeg det er fordi jeg har instansieret jeres klasser forkert.

            Overvej at lave et console output ved logevents??
            
            */
            
            //System.Console.WriteLine("Logging events to file...");

            while (true)
            {
                // need some kind of escape clause
            }
            
            

            /*
            // Test af Monitor
           Monitor monitorHandler = new Monitor();
           List<Track> trackEnter = new List<Track>();
           List<Track> trackLeft = new List<Track>();
           List<Track> trackAirspace = new List<Track>();
           List<Danger> activeDangers = new List<Danger>();
           Track track1 = new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520412");
           Track track2 = new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412");
           Track track3 = new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412");
           Track track4 = new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124520412");
           Track track5 = new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412");
           Track track6 = new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "20180304124520412");
           Track track7 = new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "20180304124520412");
           Track track8 = new Track("GSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304124520412");
           Danger danger1 = new Danger(track1, track2, 500);
           Danger danger2 = new Danger(track3, track5, 2500);
           activeDangers.Add(danger1);
           activeDangers.Add(danger2);
           trackEnter.Add(track7);
           trackEnter.Add(track8);
           trackLeft.Add(track1);
           trackLeft.Add(track2);
           trackAirspace.Add(track3);
           trackAirspace.Add(track4);
           trackAirspace.Add(track5);
           trackAirspace.Add(track6);
           trackAirspace.Add(track7);
           trackAirspace.Add(track8);
           monitorHandler.setDanger(activeDangers);
           monitorHandler.setEnter(trackEnter);
           monitorHandler.setLeave(trackLeft);
           monitorHandler.setInAir(trackAirspace);

           monitorHandler.UpdateMonitor();

    */
        }
    }
}
