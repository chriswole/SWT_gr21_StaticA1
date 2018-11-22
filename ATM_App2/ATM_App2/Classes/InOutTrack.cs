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
    public class InOutTrack : IInOutTrack
    {
        private List<Track> ListIn_ { get; set; }
        private List<Track> ListOut_ { get; set; }
        private List<TimeKeeper> TimerListIn_ { get; set; }
        private List<TimeKeeper> TimerListOut_ { get; set; }

        public event EventHandler<EnteredTrackArgs> listInUpdated;
        public event EventHandler<LeftTrackArgs> listOutUpdated;

        private readonly ILogToFile _atmLog;

        public InOutTrack(ILogToFile atmLogEvent = null)
        {
            _atmLog = atmLogEvent ?? new LogToFile();
            ListOut_=new List<Track>();
            ListIn_ = new List<Track>();
            TimerListIn_=new List<TimeKeeper>();
            TimerListOut_=new List<TimeKeeper>();
        }
        public void OnEnteredTrack(object sender, TrackArgs totrack)
        {
           TimeKeeper mytime = new TimeKeeper();
           mytime.startTimerIn();
            TimerListIn_.Add(mytime);
            ListIn_.Add(totrack.newTrack_);
            OnListInUpdated(ListIn_);
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
            mytime.startTimerOut();
            TimerListOut_.Add(mytime);
            ListOut_.Add(totrack.newTrack_);
            OnListOutUpdated(ListOut_);//do stuff
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
            TimerListIn_[0].stopTimer();
            TimerListIn_.RemoveAt(0);//RemoveAt removes 1 index

            ListIn_.RemoveAt(0);
            OnListInUpdated(ListIn_);

        }
        public void TimeElapsedOut(object sender, EventArgs e)
        {
            TimerListOut_[0].stopTimer();
            TimerListOut_.RemoveAt(0);

            ListOut_.RemoveAt(0);
            OnListOutUpdated(ListOut_);
        }
    }

}

