using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Events;

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
    class InOutTrack
    {
        private List<Track> ListIn;
        private List<Track> ListOut;
        private List<TimeKeeper> TimerListIn;
        private List<TimeKeeper> TimerListOut;
        public EventHandler<EnteredTrackArgs> listInUpdated;
        public EventHandler<LeftTrackArgs> listOutUpdated;
        public void OnEnteredTrack(object sender, TrackArgs totrack)
        {
            TimeKeeper mytime = new TimeKeeper();
            mytime.startTimer();
            TimerListIn.Add(mytime);
            ListIn.Add(totrack.newTrack_);
            OnListInUpdated(ListIn);
        }

        protected virtual void OnListInUpdated(List<Track> ListIn)
        {
            if (listInUpdated != null)
            {
                listInUpdated(this, new EnteredTrackArgs() { listEntered = ListIn });
            }
        }

        public void OnLeavingTrack(object sender, TrackArgs totrack)
        {
            TimeKeeper mytime = new TimeKeeper();
            mytime.startTimer();
            ListOut.Add(totrack.newTrack_);
            OnListOutUpdated(ListOut);//do stuff
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
            TimerListIn.Remove(TimerListIn[0]);
            ListIn.Remove(ListIn[0]);
            OnListInUpdated(ListIn);
        }
        public void TimeElapsedOut(object sender, EventArgs e)
        {
            TimerListOut[0].stopTimer();
            TimerListOut.Remove(TimerListOut[0]);
            ListOut.Remove(ListOut[0]);
            OnListOutUpdated(ListOut);
        }
    }

}

