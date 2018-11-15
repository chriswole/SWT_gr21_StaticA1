using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Interfaces;
using TransponderReceiver;

namespace ATM_App2.Classes
{

    public class StringParser: IStringParser
    {
        public StringParser() { }

        public string[] ParseDataString(string datastr, char separatorChar = ';')
        {

            string[] tokens; //Track data tokens as strings
            char[] charSeperators = new char[] { separatorChar };

            //split TransponderData into tokens by separating at ';'
            tokens = datastr.Split(charSeperators, StringSplitOptions.RemoveEmptyEntries);

            return tokens;
        }
    }

    public class BaseTrackArgs : EventArgs
    {
        public Track Track { get; set; }
    }

    public class TrackFactory
    {


        ITransponderReceiver receiver_;
        IStringParser parser_;

        public EventHandler<BaseTrackArgs> TrackCreated;

        public TrackFactory(ITransponderReceiver receiver, IStringParser parser)
        {
            this.parser_ = parser;

            this.receiver_ = receiver;
            this.receiver_.TransponderDataReady += ReceiverOnTransponderDataReady;

        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {

                //create Track for potential monitoring
                string[] tokens = parser_.ParseDataString(data, ';');
                Track track_ = BuildTrack(tokens);
                OnTrackCreated(track_);
            }

        }

        Track BuildTrack(string[] stringArr)
        {

            string tag = (string)stringArr[0];
            int posX = int.Parse(stringArr[1]);
            int posY = int.Parse(stringArr[2]);
            int altitude = int.Parse(stringArr[3]);
            string timestamp = stringArr[4];

            Track track_ = new Track(tag, new Position(posX, posY), altitude, 0, 0, timestamp);

            return track_;

        }

        protected virtual void OnTrackCreated(Track track)
        {
            if (TrackCreated != null)
                TrackCreated(this, new BaseTrackArgs() { Track = track });
        }

    }

}
