using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Events;
using ATM_App2.Interfaces;

namespace ATM_App2.Classes
{
    public class Monitor
    {
        private List<Track> trackEntering = new List<Track>();
        private List<Track> trackLeaving = new List<Track>();
        private List<Danger> activeDangers = new  List<Danger>();
        private List<Track> trackInAirspace = new List<Track>();

        public void setEnter(List<Track> listName)
        {
            trackEntering = listName;
        }

        public void setLeave(List<Track> listName)
        {
            trackLeaving = listName;
        }
        public void setDanger(List<Danger> listName)
        {
            activeDangers = listName;
        }
        public void setInAir(List<Track> listName)
        {
            trackInAirspace = listName;
        }

        //Constructor
        public Monitor()
        {

        }


        public void UpdateMonitor()
        {
            Console.Clear();
            
            Console.WriteLine("Entering tracks:");
            foreach (var obj in trackEntering)
            {
                obj.print();
            }
            
            
            Console.WriteLine("Active tracks in airspace:");
            Console.WriteLine(trackInAirspace.Count);
            
            foreach (var obj in trackInAirspace)
            {
                obj.print();
            }
            

            Console.WriteLine("Leaving tracks:");
            foreach (var obj in trackLeaving)
            {
                obj.print();
            }
            Console.WriteLine("Active Dangers:");
            foreach (var obj in activeDangers)
            {
                obj.print();
            }

        }

        
        public void OnListInUpdated(object sender, EnteredTrackArgs e)
        {
            trackEntering = e.listEntered;
            
            UpdateMonitor();
        }

        public void OnListOutUpdated(object sender, LeftTrackArgs e)
        {
            trackLeaving = e.listLeft;

            UpdateMonitor();
        }

        public void OnDangerListUpdated(object sender, DangerlistArgs e)
        {
            activeDangers = e.DangerList_;

            UpdateMonitor();
        }

        public void OnAirspaceUpdated(object sender, AirspaceTrackArgs e)
        {
            trackInAirspace = e.TracksInAirSpace;

            UpdateMonitor();
        }


    }
}
