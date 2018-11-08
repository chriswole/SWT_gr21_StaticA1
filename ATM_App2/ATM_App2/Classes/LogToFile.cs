﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Interfaces;

namespace ATM_App2.Classes
{
    public class LogToFile : ILogToFile
    {
        public LogToFile()
        {

        }
        public void AddDangerToLog(Danger danger2Log)
        {
            try
            {
                //Open or create file
                StreamWriter sw = File.AppendText("SeparationLog.txt");
                
                //Write a line of text
                sw.WriteLine("{0} and {1} Distance: {2}", danger2Log.track1_.tag_, danger2Log.track2_.tag_, danger2Log.distance_);
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                //Print out exception if it happened. 
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void AddEnteredTrackToLog(Track track2Log)
        {
            //if()
            try
            {
                //Open or create file
                StreamWriter sw = File.AppendText("SeparationLog.txt");

                //Write a line of text
                sw.WriteLine("Tag {0} has entered the airspace", track2Log.tag_ );
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                //Print out exception if it happened. 
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public void AddExitedTrackToLog(Track track2Log)
        {
            //if()
            try
            {
                //Open or create file
                StreamWriter sw = File.AppendText("SeparationLog.txt");

                //Write a line of text
                sw.WriteLine("Tag {0} has exited the airspace", track2Log.tag_);
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                //Print out exception if it happened. 
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}


