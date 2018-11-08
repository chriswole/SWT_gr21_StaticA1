using ATM_App2.Classes;

namespace ATM_App2.Interfaces
{
    public interface ILogToFile
    {
        void AddDangerToLog(Danger danger2Log);
        void AddEnteredTrackToLog(Track track2Log);
        void AddExitedTrackToLog(Track track2Log);
    }

}
