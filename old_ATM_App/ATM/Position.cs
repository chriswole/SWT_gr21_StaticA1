using System.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Interfaces
{
    public class Position
    {
        public Position(long x=0, long y=0)
        {
            x_ = x;
            y_ = y;
        }

        public long x_ { get; set; }
        public long y_ { get; set; }

        public static Position operator-(Position pos1, Position pos2)
        {
            Position pos3 = new Position();
            pos3.x_ = pos1.x_ - pos2.x_;
            pos3.y_ = pos1.y_ - pos2.y_;
            return pos3;
        }

        public static bool operator>=(Position pos1, Position pos2)
        {
            return pos1.x_ >= pos2.x_ && pos1.y_ >= pos2.y_;
        }

        public static bool operator <=(Position pos1, Position pos2)
        {

            return pos1.x_ <= pos2.x_ && pos1.y_ <= pos2.y_;
        }

        //public void print()
        //{
        //    Console.WriteLine($"x = {x_}, y = {y_}");
        //}


    }
}
