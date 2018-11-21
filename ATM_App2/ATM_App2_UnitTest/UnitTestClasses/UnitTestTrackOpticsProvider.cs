using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ATM_App2_UnitTest
{
    [TestClass]
    class UnitTestTrackOpticsProvider
    {
        private TrackOpticsProvider _uut;

        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {
            _uut = new TrackOpticsProvider();

        }

        [TestCase(0,0,0, 100, 100.0)]
        public void GetDistanceBetweenTracksTest(int x1, int y1, int x2, int y2, double result)
        {

            //setup
            Track firsdTrack = new Track("", new Position(x1, y1), 0, 0, 0, "");
            Track secondTrack = new Track("", new Position(x2, y2), 0, 0, 0, "");

            //Act
            double distance = _uut.GetDistanceBetweenTracks(firsdTrack, secondTrack);

            //Assert
            Assert.That(distance, Is.EqualTo(result));  
        }


        [TestCase(0, 0, "2018111519330000", 0, 100, "2018111519330100", 100.0)]
        public void GetTrackVelocityTest(int x1, int y1, string timestamp1, 
                                         int x2, int y2, string timestamp2, double result)
        {
            //setup
            Track firsdTrack = new Track("", new Position(x1, y1), 0, 0, 0, timestamp1);
            Track secondTrack = new Track("", new Position(x2, y2), 0, 0, 0, timestamp2);

            //Act
            double velocity = _uut.GetTrackVelocity(firsdTrack, secondTrack);

            //Assert
            Assert.That(velocity, Is.EqualTo(result));

            
        }

        
        [TestCase(0,10, 0, 0, 0)]
        [TestCase(10,0, 0, 0, 90)]
        [TestCase(0,-10, 0, 0, 180)]
        [TestCase(-10, 0, 0, 0, 270)]

        public void GetTrackCourseTest(int x1, int y1, int x2, int y2, double result)
        {
            //setup
            Track firsdTrack = new Track("", new Position(x1, y1), 0, 0, 0, "");
            Track secondTrack = new Track("", new Position(x2, y2), 0, 0, 0, "");

            //Act
            double course = _uut.GetTrackCourse(firsdTrack, secondTrack);

            //Assert
            Assert.That(course, Is.EqualTo(result));
        }
    }
}
