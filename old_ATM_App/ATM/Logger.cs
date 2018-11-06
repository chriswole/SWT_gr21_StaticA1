using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ATM.Interfaces;

namespace ATM
{
    class Logger
    {
        public void AddToLog(Danger danger2Log)
        {
            try
            {
                
                //Open or create file
                StreamWriter sw = File.AppendText("SeparationLog.txt");

                
                //Write a line of text
                sw.WriteLine("%s and %s Distance: %d", danger2Log.track1_.tag_, danger2Log.track2_.tag_, danger2Log.distance_);
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
