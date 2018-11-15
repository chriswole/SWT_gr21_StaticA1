using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using NSubstitute;
using NUnit.Framework;

namespace ATM_App2_UnitTest
{
    class UnitTestTrackOpticsProvider
    {
        private TrackOpticsProvider _uut;

        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {
            _uut = new TrackOpticsProvider();

        }

        [Test]
        public void GetDistanceBetweenTracksTest()
        {

            //setup
            #region 
            Track[]_tracksForDistanceTests = new Track[]
            {
                new Track("", new Position(0,0), 0, 0, 0, "")
            };

            Track[] _tracksForDistanceComparison_Tests = new Track[]
            {
                new Track("", new Position(0, 100), 0, 0, 0, "")
            };

            double[] _results = new double[]
            {
                100.0
            };

            #endregion 

            //Track track1 = new Track("", new Position(0, 0), 0, 0, 0, "");
            //Track track2 = new Track("", new Position(0, 100), 0, 0, 0, "");

            for (int i = 0; i < _tracksForDistanceTests.Length; i++)
            {
                double distance = _uut.GetDistanceBetweenTracks(_tracksForDistanceTests[i], _tracksForDistanceComparison_Tests[i]);

                Assert.That(distance, Is.EqualTo(_results[i]));
            } 
           
        }

        [Test]
        public void GetTrackVelocityTest()
        {
            Track[] _tracksForVelocityTest = new Track[]
            {
                new Track("", new Position(0,0), 0, 0, 0, "20181115193300")
            };

            Track[] _comparisonTracksForVelocityTests = new Track[]
            {
                new Track("", new Position(0, 100), 0, 0, 0, "20181115193301")
            };

            double[] _results = new double[]
            {
                100.0
            };

            //Track track1 = new Track("", new Position(0, 0), 0, 0, 0, "");
            //Track track2 = new Track("", new Position(0, 100), 0, 0, 0, "");

            for (int i = 0; i < _tracksForVelocityTest.Length; i++)
            {
                double distance = _uut.GetTrackVelocity(_tracksForVelocityTest[i], _comparisonTracksForVelocityTests[i]);

                Assert.That(distance, Is.EqualTo(_results[i]));
            }
        }

        [Test]
        public void GetTrackCourseTest()
        {
            //setup
            #region 
            Track[] _tracksCourseTest = new Track[]
            {
                new Track("", new Position(100,100), 0, 0, 0, ""),
                new Track("", new Position(0,10), 0, 0, 0, ""),
                new Track("", new Position(0,10), 0, 0, 0, "")
            };

            Track[] _comparisonTracksForCourseTests = new Track[]
            {
                new Track("", new Position(100, 100), 0, 0, 0, ""),
                new Track("", new Position(0,0), 0, 0, 0, ""),
                new Track("", new Position(0,-10), 0, 0, 0, "")

            };

            double[] _results = new double[]
            {
                0, 90, 180
            };

            #endregion 

            for (int i = 0; i < _comparisonTracksForCourseTests.Length; i++)
            {
                double distance = _uut.GetTrackCourse(_tracksCourseTest[i], _comparisonTracksForCourseTests[i]);

                Assert.That(distance, Is.EqualTo(_results[i]));
            }
        }
    }
}
