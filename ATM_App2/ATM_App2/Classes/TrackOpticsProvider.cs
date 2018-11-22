using System;
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
       

    }

    public class TrackOpticsProvider : ITrackOpticsProvider
    {
        public double GetDistanceBetweenTracks(Track firsTrack, Track secondTrack)
        {
            
            var dist = secondTrack.pos_ - firsTrack.pos_;
            var distance = Math.Sqrt((dist.x_ * dist.x_) + (dist.y_ * dist.y_));
            if (distance < 0) distance  *= -1.0;

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
            double deltaX = activeTrack.pos_.x_ - passiveTrack.pos_.x_;
            double deltaY = activeTrack.pos_.y_ - passiveTrack.pos_.y_;
            double course = Math.Atan2(deltaX, deltaY) * (180.0 /Math.PI);

            if (course < 0)
                course += 360.0;
            
            return course;
        }

        private double GetSecondsBetweenTimeStamps(string oldTimeStamp, string newTimeStamp)
        {
            
           DateTime firstTime = DateTime.ParseExact(oldTimeStamp, "yyyyMMddHHmmssff",

               new DateTimeFormatInfo());


            DateTime secondTime = DateTime.ParseExact(newTimeStamp, "yyyyMMddHHmmssff",
                new DateTimeFormatInfo());


            var seconds = (secondTime - firstTime).TotalSeconds;
            if (seconds < 0) seconds *= -1.0;

            return seconds;
        }

        


        
    }
}
