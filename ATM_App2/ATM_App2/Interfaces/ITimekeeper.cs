using System;

namespace ATM_App2.Interfaces
{
    public interface ITimekeeper
    {
        void startTimerIn();
        void startTimerOut();
        void stopTimer();
        
    }
}