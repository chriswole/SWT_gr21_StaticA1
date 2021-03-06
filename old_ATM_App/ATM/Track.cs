﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM
{
    public class Track //make track
    {
        public Track(string tag = "", Position pos = null, int altitude = 0, int horiVelocity = 0, int course = 0,
            string timestamp = "" )
        {
            tag_ = tag;
            pos_ =  pos == null ? new Position(0,0) : pos;
            altitude_ = altitude;
            hori_velocity_ = horiVelocity;
            course_ = course;
            timestamp_ = timestamp;
        }

        public string tag_ { get; set; }
        public Position pos_ { get; set; }
        public int altitude_ { get; set; }
        public int hori_velocity_ { get; set; }
        public int course_ { get; set; }
        public string timestamp_{get; set;}
        

        /*
    public override string ToString()
        {
            string str = $"Track:\n " +
                         $"{tag_}\n " +
                         $"{pos_.x_},{pos_.y_}\n " +
                         $"{altitude_}\n " +
                         $"{hori_velocity_}\n " +
                         $"{timestamp_}\n";
            return str;
        }*/

       public static bool operator==(Track track1, Track track2)
        {
            
            if (track1.tag_ == track2.tag_ &&
                track1.pos_.x_ == track2.pos_.x_ &&
                track1.pos_.y_ == track2.pos_.y_ &&
                track1.altitude_ == track2.altitude_ &&
                track1.hori_velocity_ == track2.hori_velocity_ &&
                track1.course_ == track2.course_ &&
                track1.timestamp_ == track2.timestamp_)
                return true;
            else
            {
                return false;
            }


        }

        public static bool operator !=(Track track1, Track track2)
        {
            
            if (track1.tag_ == track2.tag_ &&
                track1.pos_.x_ == track2.pos_.x_ &&
                track1.pos_.y_ == track2.pos_.y_ &&
                track1.altitude_ == track2.altitude_ &&
                track1.hori_velocity_ == track2.hori_velocity_ &&
                track1.course_ == track2.course_ &&
                track1.timestamp_ == track2.timestamp_)
                return false;
            else
            {
                return true;
            }

        }
        


    }
}
