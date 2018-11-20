using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using ATM_App2.Classes;
using ATM_App2.Interfaces;

//from https://docs.microsoft.com/en-us/dotnet/api/system.timers.timer.elapsed?view=netframework-4.7.2
namespace ATM_App2.Events
{

    public class TrackEnteredAirspace : ATMNotification
    {
        private readonly ILogToFile _atmLog;
        public EventHandler<EnteredTrackArgs> listInUpdated;

        public TrackEnteredAirspace(ILogToFile atmLogEvent = null)
        {
            _atmLog = atmLogEvent ?? new LogToFile();
        }

        //public override void DetectNotification(Collection<Track> oldTransponderDatas, Collection<Track> newTransponderDatas)
        //{
        //    foreach (var item in newTransponderDatas.Where(item => oldTransponderDatas.All(t => t.tag_ != item.tag_)))
        //    {
        //        const string logString = " TrackEnteredAirspace Notification ";
        //        Notify(new NotificationEventArgs(item.tag_, "TrackEnteredAirspace", item.timestamp_));
        //        _atmLog.Log(item.timestamp_ + logString + item.tag_);
        //    }
        //}

        public void OnEnteredTrack(object sender, TrackArgs totrack)
        {
            Notify(new NotificationEventArgs(totrack.newTrack_.tag_, "TrackEnteredAirspace", totrack.newTrack_.timestamp_));
            _atmLog.Log(totrack.newTrack_.timestamp_ + totrack.newTrack_.tag_ + " entereed"); 
        }
    }
}

