﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATM_App2.Classes;

namespace ATM_App2.Classes
{
    public interface ITrackOpticsProvider
    {
        double GetDistanceBetweenTracks(Track firsTrack, Track secondTrack);
        double GetTrackVelocity(Track activeTrack, Track passiveTrack);
        double GetTrackCourse(Track activeTrack, Track passTrack);
        List<Danger> RemoveOldDangers(Track newTrack, List<Danger> dangerList);

    }

    public class TrackOpticsProvider : ITrackOpticsProvider
    {
        public double GetDistanceBetweenTracks(Track firsTrack, Track secondTrack)
        {
            
            var dist = firsTrack.pos_ - secondTrack.pos_;
            var distance = Math.Sqrt((dist.x_ * dist.x_) + (dist.y_ * dist.y_));

            return distance;
        }


        
        public double GetTrackVelocity(Track activeTrack, Track passiveTrack)
        {
            var distance = GetDistanceBetweenTracks(activeTrack, passiveTrack);
            var time = GetSecondsBetweenTimeStamps(activeTrack.timestamp_, passiveTrack.timestamp_);

            var velocity = distance / time;

            return velocity;
        }   

        public double GetTrackCourse(Track activeTrack, Track passiveTrack)
        {
            var angle = Math.Atan2(activeTrack.pos_.y_, activeTrack.pos_.x_) -
                        Math.Atan2(passiveTrack.pos_.y_, passiveTrack.pos_.x_);

            angle = angle * 360.0 / (2 * Math.PI);
            //if (angle < 0)
            //{
            //    angle = angle + 360.0;
            //}
            angle = ((angle % 360) + 360) % 360;

            return angle;
        }

        private double GetSecondsBetweenTimeStamps(string oldTimeStamp, string newTimeStamp)
        {
            
           DateTime firstTime = DateTime.ParseExact(oldTimeStamp, "yyyyMMddHHmmss",

               new DateTimeFormatInfo());


            DateTime secondTime = DateTime.ParseExact(newTimeStamp, "yyyyMMddHHmmss",

                new DateTimeFormatInfo());


            var seconds = (secondTime - firstTime).TotalSeconds;

            return seconds;
        }

        public List<Danger> RemoveOldDangers(Track newTrack, List<Danger> dangerList)
        {


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
                        }
                    }
                }
            }
            
            return dangerList;
        }


        
    }
}
