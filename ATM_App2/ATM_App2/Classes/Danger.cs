using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App2.Classes
{
    public class Danger
    {
        //Constructor
        public Danger(Track track1, Track track2, int dist)
        {
            track1_ = track1;
            track2_ = track2;
            distance_ = dist;
        }
        // the tracks that are closing in and  their distance
        public Track track1_ { get; private set; }
        public Track track2_ { get; private set; }
        public int distance_ { get; private set; }
        public int altDistance_ { get; private set; }
        // the print function for Danger
        // the altitude is per definition less than 300, otherwise danger isn't created. 
        //public void print()
        //{
        //    Console.Write(track1_.tag_);
        //    Console.Write(" and ");
        //    Console.Write(track2_.tag_);
        //    Console.Write(" Distance: ");
        //    Console.WriteLine(distance_);

        //}
        public static bool operator ==(Danger danger1, Danger danger2)
        {

            if (danger1.track1_.tag_ == danger2.track1_.tag_ &&
                danger1.track2_ == danger2.track2_ &&
                danger1.distance_ == danger2.distance_)
                return true;
            else
            {
                return false;
            }


        }
        public static bool operator !=(Danger danger1, Danger danger2)
        {

            if (danger1.track1_ != danger2.track1_ ||
                danger1.track2_ != danger2.track2_ ||
                danger1.distance_ != danger2.distance_)
                return true;
            else
            {
                return false;
            }


        }
    }
}
