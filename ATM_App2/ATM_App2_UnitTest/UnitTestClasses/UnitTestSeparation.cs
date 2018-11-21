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
            fakeInAirSpaceObserver_ = Substitute.For<IInAirSpaceObserver>();
            uut_ = new Separation(fakeLogToFile_, fakeOpticsProvider_);

            fakeInAirSpaceObserver_.AirspaceUpdated += uut_.OnAirspaceUpdated;

        }


        #region OnAirSpaceUpdated_Tests
        [Test]
        public void OnAirSpaceUpdated_NoSeparationInList_NoDangersMade()
        {
            // Arrange 
            List<Track> trackList = new List<Track>();
            trackList.Insert(0, new Track("track1", new Position(15000, 40000), 500, 1000, 30, "20180403103240"));
            trackList.Insert(0, new Track("track2", new Position(50000, 20000), 800, 1000, 120, "20180403103241"));
            

            int eventCounter = 0;


            uut_.DangerListUpdated += (o, args) => { eventCounter++; };
            
            //Act 
            //Need to raise event after each Track added, to make certain all tracks have been compared.
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, new Track("track3", new Position(80000, 65000), 4700, 1000, 210, "20180403103243"));
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, new Track("track4", new Position(37000, 86000), 12000, 1000, 300, "20180403103245"));
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));

            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));

        }

        [Test]
        public void OnAirSpaceUpdated_1SeparationInList_1DangersMade()
        {
            // Arrange 
            List<Danger> Dangerlist = new List<Danger>();
            List<Track> trackList = new List<Track>();
            Track track1 = new Track("track1", new Position(15000, 40000), 500, 1000, 30, "20180403103240");
            Track track2 = new Track("track2", new Position(50000, 20000), 850, 1000, 120, "20180403103241");
            Track track3 = new Track("track3", new Position(80000, 65000), 4700, 1000, 210, "20180403103243");
            Track track4 = new Track("track4", new Position(12000, 41000), 300, 1000, 300, "20180403103245");
            Danger testdanger = new Danger(track1, track4, 3162); // distance calculated in mathcad. 
            trackList.Insert(0, track1 );
            trackList.Insert(0, track2 );

            int eventCounter = 0;
            
            uut_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            //Act 
            //Need to raise event after each Track added, to make certain all tracks have been compared.
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track3);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track4);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            
            //Assert
            Assert.That(eventCounter, Is.EqualTo(1));
            Assert.That(Dangerlist.Count, Is.EqualTo(1));
            Assert.That(Dangerlist[0]==testdanger, Is.True);

        }

        [Test]
        public void OnAirSpaceUpdated_XYSeparation_NotInAltitude_NoDangersMade()
        {
            // Arrange 
            List<Danger> Dangerlist = new List<Danger>();
            List<Track> trackList = new List<Track>();
            Track track1 = new Track("track1", new Position(15000, 40000), 500, 1000, 30, "20180403103240");
            Track track2 = new Track("track2", new Position(50000, 20000), 800, 1000, 120, "20180403103241");
            Track track3 = new Track("track3", new Position(80000, 65000), 4700, 1000, 210, "20180403103243");
            Track track4 = new Track("track4", new Position(12000, 41000), 3000, 1000, 300, "20180403103245");
            Danger testdanger = new Danger(track1, track4, 3162); // distance calculated in mathcad. 

            trackList.Insert(0, track1);
            trackList.Insert(0, track2);


            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            uut_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };


            //Act 
            //Need to raise event after each Track added, to make certain all tracks have been compared.
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track3);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track4);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));

            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));
            Assert.That(Dangerlist.Count, Is.EqualTo(0));

        
        }

        [Test]
        public void OnAirSpaceUpdated_Separation_NotInAltitude_NoDangersMade()
        {
            // Arrange 
            List<Danger> Dangerlist = new List<Danger>();
            List<Track> trackList = new List<Track>();
            Track track1 = new Track("track1", new Position(15000, 40000), 500, 1000, 30, "20180403103240");
            Track track2 = new Track("track2", new Position(50000, 20000), 800, 1000, 120, "20180403103241");
            Track track3 = new Track("track3", new Position(80000, 65000), 4700, 1000, 210, "20180403103243");
            Track track4 = new Track("track4", new Position(12000, 70000), 600, 1000, 300, "20180403103245");
            Danger testdanger = new Danger(track1, track4, 3162); // distance calculated in mathcad. 

            trackList.Insert(0, track1);
            trackList.Insert(0, track2);


            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            uut_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };


            //Act 
            //Need to raise event after each Track added, to make certain all tracks have been compared.
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track3);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track4);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));

            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));
            Assert.That(Dangerlist.Count, Is.EqualTo(0));


        }


        // List<Danger> Dangerlist = new List<Danger>();
        // uut_.DangerListUpdated += (o,args) => { Dangerlist = args.DangerList_; }; // for at "kigge på Dangers sendt til monitor

        [Test]
        public void OnAirSpaceUpdated_make1Danger_Remove1Danger()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            List<Track> trackList = new List<Track>();
            Track track1 = new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520412");
            Track track2 = new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412");
            Track track3 = new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412");
            Track track8 = new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304124520412");

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            uut_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };



            trackList.Insert(0, track1);
            trackList.Insert(0, track2);
            

            //Act 
            //Need to raise event after each Track added, to make certain all tracks have been compared.
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track3);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track8);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));

            //Assert
            Assert.That(eventCounter,Is.EqualTo(2));
            Assert.That(Dangerlist.Count,Is.EqualTo(0));


        }

        [Test]
        public void OnAirSpaceUpdated_make2Danger_update1Danger()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            List<Track> trackList = new List<Track>();
            Track track1 = new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520412");
            Track track2 = new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412");
            Track track3 = new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412");
            Track track8 = new Track("HSAN329", new Position(24500, 11000), 700, 0, 0, "20180304124520412");

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            uut_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };



            trackList.Insert(0, track1);
            trackList.Insert(0, track2);


            //Act 
            //Need to raise event after each Track added, to make certain all tracks have been compared.
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track3);
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));
            trackList.Insert(0, track8);
            trackList.Remove(track1);// replaced by track 8 
            fakeInAirSpaceObserver_.AirspaceUpdated += Raise.EventWith(this, new AirspaceTrackArgs(trackList));

            //Assert
            Assert.That(eventCounter, Is.EqualTo(2));

            Assert.That(Dangerlist.Count, Is.EqualTo(1));


        }



        #endregion

        #region RemoveDangerTests

        [Test]
        public void RemoveOldDangersTest_1DangerToRemove_1DangerLeftInList()
        {
            //Arrange
            Track track1 = new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520412");
            Track track2 = new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412");
            Track track3 = new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412");
          //  Track track4 = new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124520412");
            Track track5 = new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412");
          //  Track track6 = new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "20180304124520412");
          //  Track track7 = new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "20180304124520412");
            Track track8 = new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304124520412");

            // Dangers  1-2, 3-5

            Danger D1 = new Danger(track1,track2,1802);
            Danger D2 = new Danger(track3,track5,1188);

            List<Danger> Dlist = new List<Danger>();
            Dlist.Add(D1);
            Dlist.Add(D2);

            //Act
            Dlist= uut_.RemoveOldDangers(track8,Dlist);

            // Assert
            Assert.That(Dlist.Count,Is.EqualTo(1));
        }

        #endregion
    }
}

/*
//Arrange
Track track1 = new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520412");
Track track2 = new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412");
Track track3 = new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412");
Track track4 = new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124520412");
Track track5 = new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412");
Track track6 = new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "20180304124520412");
Track track7 = new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "20180304124520412");
Track track8 = new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304124520412");


*/