﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Events;
using ATM_App2.Interfaces;
using NUnit.Framework.Internal;

namespace ATM_App2.Classes
{
    public class DangerlistArgs : EventArgs
    {
        public List<Danger> DangerList_ { get; set; }

    
    }

 
        public class Separation : ISeparation
        {
            private List<Danger> Dangers_ { get; set; }
            private ITrackOpticsProvider opticsProvider_;
            private ILogToFile loggerToFile_;


            // Eventhandler to subscribe on
            public event EventHandler<DangerlistArgs> DangerListUpdated;
            public void OnAirspaceUpdated(object sender, AirspaceTrackArgs argList)
            {
                CalculateDistances(argList.TracksInAirSpace);
            }

       
            public Separation(ILogToFile logger2File, ITrackOpticsProvider opticsProvider)
            {
                Dangers_ = new List<Danger>();
                opticsProvider_ = opticsProvider;
                //?? returns the left side if not null, if null right side is returned.
                loggerToFile_ = logger2File ?? new LogToFile();
            }


            public void CalculateDistances(List<Track> trackList)
            {
                var newTrack = trackList[0];
                if (Dangers_.Count!=0)
                {
                    Dangers_ = RemoveOldDangers(newTrack, Dangers_);
                }
               
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
                                int i = 0;
                                Danger dangerObj = new Danger(newTrack, track1, (int) distance);
                                foreach (var dangerObject in Dangers_)
                                {
                                    if (dangerObj.track1_ == dangerObject.track1_ && dangerObj.track2_ == dangerObject.track2_)
                                    {
                                        Dangers_[i] = dangerObj;
                                        // lav Danger list updated event her. 
                                        OnDangerListUpdated(Dangers_);
                                }
                                    else
                                    {
                                        Dangers_.Add(dangerObj);
                                        // lav Danger list updated event her. 
                                        OnDangerListUpdated(Dangers_);
                                }

                                    i++;
                                }

                            }
                        }
                    }
                   
                    
                }
            }


        public List<Danger> RemoveOldDangers(Track newTrack, List<Danger> dangerList)
        {
            bool changedList = false;

            foreach (var dan in dangerList)
            {   // if changed track is track1_
                if (newTrack.tag_ == dan.track1_.tag_)
                {
                    // Are the planes within same altitude layer
                    int alt = Math.Abs(newTrack.altitude_ - dan.track1_.altitude_);
                    if (alt > 300)
                    {
                        // are the planes too close in xy-plane
                        Position dist = newTrack.pos_ - dan.track1_.pos_;
                        var distance = Math.Sqrt((dist.x_ * dist.x_) + (dist.y_ * dist.y_));
                        if (distance > 5000)
                        {
                            // remove danger from list, since it is not danger anymore
                            dangerList.Remove(dan);
                            changedList = true;
                        }
                    }
                }
                else if (newTrack.tag_ == dan.track2_.tag_)
                {   // if changed track is track2_
                    int alt = Math.Abs(newTrack.altitude_ - dan.track2_.altitude_);
                    if (alt > 300)
                    {
                        // are the planes too close in xy-plane
                        Position dist = newTrack.pos_ - dan.track2_.pos_;
                        var distance = Math.Sqrt((dist.x_ * dist.x_) + (dist.y_ * dist.y_));
                        if (distance > 5000)
                        {
                            // remove danger from list, since it is not danger anymore
                            dangerList.Remove(dan);
                            changedList = true;
                        }
                    }
                }
            }

            if (changedList)
            {
                OnDangerListUpdated(dangerList);
            }
            return dangerList;
        }

        protected virtual void OnDangerListUpdated(List<Danger> dangers)
            {
                if (DangerListUpdated != null)
                {
                    DangerListUpdated(this, new DangerlistArgs(){DangerList_ = dangers});
                }
            }
            


        }
   
}
