using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


namespace ATM
{
    public class Separation : ISeparation
    {
        public List<Danger> newDangers_ { get; set; }
        public List<Danger> OldDangers_ { get; set; }
        

        public Separation()
        {
            newDangers_ = new List<Danger>();
            OldDangers_ = new List<Danger>();
        }

        public void calculateDistances(List<Track> trackList)
        {
            foreach (var track1 in trackList)
            {
                foreach (var track2 in trackList)
                {
                    // Are the planes within same altitude layer
                    int alt = Math.Abs(track1.altitude_ - track2.altitude_);
                    if (alt < 300)
                    {
                        // are the planes too close in xy-plane
                        Position dist = track1.pos_ - track2.pos_;
                        var distance = Math.Sqrt((dist.x_*dist.x_) + (dist.y_*dist.y_));
                        if (distance < 5000)
                        {
                            // Is the planes actually the same - if not make a dangerObj
                            if (track1.tag_ != track2.tag_)
                            {
                                Danger dangerObj = new Danger(track1, track2, (int) distance);
                                newDangers_.Add(dangerObj);
                            }
                        }
                    }
                }
            }
        }
        



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


        }


    }
}
