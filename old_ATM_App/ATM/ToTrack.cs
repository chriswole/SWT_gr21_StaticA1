using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM
{
    public class ToTrack 
    {
        public List<Track> Tracks { get; set; }

        private ITransponderReceiver receiver_;

        //Air space definition:
        private Position southWestCorner = new Position(10000, 10000), northEastCorner = new Position(90000, 90000);
        private int lowest_altitude = 500, highest_altitude = 20000;

        public ToTrack(ITransponderReceiver receiver)
        {
            Tracks = new List<Track>();

            //This will store the real or the fake transponder data receiver
            this.receiver_ = receiver;

            //Attach to the event of the real or the fake TDR
            this.receiver_.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {

                //create Track for potential monitoring
                string[] tokens = ParseDataString(data);
                Track track_ = BuildTrack(tokens);

                bool inScope_ = InScope(track_);
                bool found = false;
                if (inScope_)
                {
                    for (int i = 0; i < Tracks.Count; i++)
                        if (track_.tag_ == Tracks[i].tag_)
                        {
                            found = true;
                            System.Console.WriteLine("Track found i Tacks List");
                            if (track_ != Tracks[i])
                            {
                                System.Console.WriteLine("Updating Track");
                                Tracks[i] = track_;
                            }

                        }

                    if (!found)
                        Tracks.Add(track_);
                }


            }

        }

        //Helper function to separate Transponder data:
        public string[] ParseDataString(string datastr)
        {

            string[] tokens; //Track data tokens as strings
            char[] charSeperators = new char[] {';'};

            //split TransponderData into tokens by separating at ';'
            tokens = datastr.Split(charSeperators, StringSplitOptions.RemoveEmptyEntries);

            return tokens;
        }

        //Helper function to build Track from tokens
        public Track BuildTrack(string[] stringArr)
        {

            Track track_ = new Track();

            track_.tag_ = (string) stringArr[0];
            track_.pos_.x_ = int.Parse(stringArr[1]);
            track_.pos_.y_ = int.Parse(stringArr[2]);
            track_.altitude_ = int.Parse(stringArr[3]);
            track_.timestamp_ = stringArr[4];

            return track_;

        }

        public bool InScope(Track track)
        {
            return track.pos_ >= (Position) southWestCorner && track.pos_ <= (Position) northEastCorner &&
                   track.altitude_ >= lowest_altitude && track.altitude_ <= highest_altitude;

        }
    }
}
