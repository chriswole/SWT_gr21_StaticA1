using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Interfaces;

namespace ATM_App2.Events
{
    public class TrackLeftAirspace : ATMNotification
    {
        private readonly ILogToFile _atmLog;

        public TrackLeftAirspace(ILogToFile atmLogEvent = null)
        {
            _atmLog = atmLogEvent ?? new LogToFile();
        }
        
        public override void DetectNotification(Collection<Track> oldTransponderDatas, Collection<Track> newTransponderDatas)
        {
            foreach (var item in oldTransponderDatas.Where(item => newTransponderDatas.All(t => t.tag_ != item.tag_)))
            {
                const string logString = " TrackLeftAirspace Notification ";
                Notify(new NotificationEventArgs(item.tag_, "TrackLeftAirspace", item.timestamp_));
                _atmLog.Log(item.timestamp_ + logString + item.tag_);
            }
        }

    }
}
