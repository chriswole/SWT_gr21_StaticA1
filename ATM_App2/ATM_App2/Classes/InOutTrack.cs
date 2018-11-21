using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Events;
using ATM_App2.Interfaces;

namespace ATM_App2.Classes
{
    public class EnteredTrackArgs : EventArgs
    {
        public List<Track> listEntered { get; set; }
    }
    public class LeftTrackArgs : EventArgs
    {
        public List<Track> listLeft { get; set; }
    }
    public class InOutTrack:IInOutTrack
    {
        private List<Track> ListIn;
        private List<Track> ListOut;
        private List<TimeKeeper> TimerListIn;
        private List<TimeKeeper> TimerListOut;
       
        public event EventHandler<EnteredTrackArgs> listInUpdated;
        public event EventHandler<LeftTrackArgs> listOutUpdated;

        private readonly ILogToFile _atmLog;

        public InOutTrack(ILogToFile atmLogEvent = null)
        {
            _atmLog = atmLogEvent ?? new LogToFile();
        }
        public void OnEnteredTrack(object sender, TrackArgs totrack)
        {
            List<TimeKeeper> TimerListIn = new List<TimeKeeper>();
            List<Track> ListIn = new List<Track>();
            TimeKeeper mytime = new TimeKeeper();
            mytime.startTimerIn();
            TimerListIn.Add(mytime);
            ListIn.Add(totrack.newTrack_);
            OnListInUpdated(ListIn);
            _atmLog.Log(totrack.newTrack_.timestamp_ + totrack.newTrack_.tag_ + " entereed");
        }

        protected virtual void OnListInUpdated(List<Track> ListIn)
        {
            if (listInUpdated != null)
            {
                listInUpdated(this, new EnteredTrackArgs() { listEntered = ListIn });
            }
            return;
        }



        public void OnLeavingTrack(object sender, TrackArgs totrack)
        {
            TimeKeeper mytime = new TimeKeeper();
            List<Track> ListOut = new List<Track>();
            List<TimeKeeper> TimerListOut = new List<TimeKeeper>();
            mytime.startTimerOut();
            TimerListOut.Add(mytime);
            ListOut.Add(totrack.newTrack_);
            OnListOutUpdated(ListOut);//do stuff
            _atmLog.Log(totrack.newTrack_.timestamp_ + totrack.newTrack_.tag_ + " left");
        }
        protected virtual void OnListOutUpdated(List<Track> ListOut)
        {
            if (listOutUpdated != null)
            {
                listOutUpdated(this, new LeftTrackArgs() { listLeft = ListOut });
            }
        }

        public void TimeElapsedIn(object sender, EventArgs e)
        {
            TimerListIn[0].stopTimer();
            TimerListIn.RemoveAt(0);//RemoveAt removes 1 index

            ListIn.RemoveAt(0);
            OnListInUpdated(ListIn);

        }
        public void TimeElapsedOut(object sender, EventArgs e)
        {
            TimerListOut[0].stopTimer();
            TimerListOut.RemoveAt(0);
            ListOut.RemoveAt(0);
            OnListOutUpdated(ListOut);
        }
    }

}

