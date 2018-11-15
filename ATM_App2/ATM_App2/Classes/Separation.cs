using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App2.Classes
{
    
        public class Separation //: ISeparation
        {
            private List<Danger> Dangers_ { get; set; }
            private List<Track> receivedTrack_ { get; set; }

           /* public void OnAirspaceUpdated(object sender, AirspaceTrackArgs arg)
            {

            }
        */
            public Separation()
            {
                Dangers_ = new List<Danger>();
            }


            public void CalculateDistances(List<Track> trackList)
            {
                var newTrack = trackList[0];
                foreach (var track1 in trackList)
                {
                   
                    // Are the planes within same altitude layer
                    int alt = Math.Abs(newTrack.altitude_ - track1.altitude_);
                    if (alt < 300)
                    {
                        // are the planes too close in xy-plane
                        Position dist = newTrack.pos_ - track1.pos_;
                        var distance = Math.Sqrt((dist.x_ * dist.x_) + (dist.y_ * dist.y_));
                        if (distance < 5000)
                        {
                            // Is the planes actually the same - if not make a dangerObj
                            if (newTrack.tag_ != track1.tag_)
                            {
                                Danger dangerObj = new Danger(newTrack, track1, (int)distance);
                                Dangers_.Add(dangerObj);
                            }
                        }
                    }
                }
            }

            

            /*
            public void deactivateAlarm()
            {
                if (OldDangers_.SequenceEqual(newDangers_))
                {
                    //Clears the OldDanger List, so the new dangers can be added
                    OldDangers_.Clear();
                }
                else
                {
                    //Compares newDangers_ with OldDangers_ and removes matching elements from OldDangers_
                    OldDangers_.Except(newDangers_);
                }
            }
            */

            /*
            public void raiseAlarm()
            {


                foreach (var dangerObj in newDangers_)
                {
                    //Compares newDangers_ with OldDangers_
                    if (newDangers_.SequenceEqual(OldDangers_))
                    {
                        //If Equal, print the tracks
                        newDangers_.ForEach(danger => Console.Write("Warning, the following planes are too close!\n{0}", newDangers_));
                    }

                    else
                    {
                        //If not equal, add newDangers to OldDangers
                        newDangers_.AddRange(OldDangers_);

                        //Add new element to Logger
                        new Logger().AddToLog(dangerObj);

                        //Compares newDangers_ with OldDangers_ and removes matching elements from newDangers_
                        newDangers_.Except(OldDangers_);
                    }

                }
            }*/
        }
   
}
