using System;
using System.Collections.Generic;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ATM_App2_UnitTest
{
    [TestClass]
    public class UnitTestSeparation
    {
        private IInAirSpaceObserver fakeInAirSpaceObserver_; 
        private ITrackOpticsProvider fakeOpticsProvider_;
        private ILogToFile fakeLogToFile_;
        private Separation uut_;

        [SetUp]
        public void Setup()
        {
            fakeOpticsProvider_ = Substitute.For<ITrackOpticsProvider>();
            fakeLogToFile_ = Substitute.For<ILogToFile>();
            uut_ = new Separation(fakeLogToFile_, fakeOpticsProvider_);

            fakeInAirSpaceObserver_.AirspaceUpdated += uut_.OnAirspaceUpdated;

        }





        #region OnAirSpaceUpdated_Tests
        [Test]
        public void OnAirSpaceUpdated_NoSeparationInList_NoDangersMade()
        {
            // Arrange 
            List<Track> trackList = new List<Track>();
            trackList.Add(new Track("track1", new Position(15000, 40000), 500, 1000, 30, "20180403103240"));
            trackList.Add(new Track("track2", new Position(50000, 20000), 800, 1000, 120, "20180403103241"));
            trackList.Add(new Track("track3", new Position(80000, 65000), 4700, 1000, 210, "20180403103243"));
            trackList.Add(new Track("track4", new Position(37000, 86000), 12000, 1000, 300, "20180403103245"));

            int eventCounter = 0;


            uut_.DangerListUpdated += (o, args) => { eventCounter++; };
            
            //Act 
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));


            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));

        }

        // uut_.DangerListUpdated += (o,args) => { Dangerlist = args.DangerList_; }; // for at "kigge på Dangers sendt til monitor



        #endregion


    }
}
