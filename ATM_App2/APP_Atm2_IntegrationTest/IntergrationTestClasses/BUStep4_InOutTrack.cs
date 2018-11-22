using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ATM_App2.Classes;
using ATM_App2.Events;
using ATM_App2.Interfaces;
using Castle.Components.DictionaryAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace APP_Atm2_IntegrationTest.IntergrationTestClasses
{
    [TestFixture]
    [TestClass]
    public class BUStep4_InOutTrack
    {
        private IInOutTrackHandler InOutHandler_;
        private IInAirSpaceObserver inAirSpaceObserver_;
        private ITrackOpticsProvider trackOpticsProvider_;
        private ILogToFile atmLog_;
        private ISeparation separationControl_;

        private ITrackFactory fakeTrackFactory_;

        [SetUp]
        public void Setup()
        {
            atmLog_ = new LogToFile();
            trackOpticsProvider_=new TrackOpticsProvider();
            inAirSpaceObserver_=new InAirSpaceObserver(trackOpticsProvider_);
            InOutHandler_ = new InOutTrackHandler(atmLog_);
            separationControl_ = new Separation(atmLog_);

            fakeTrackFactory_ = Substitute.For<ITrackFactory>();

            inAirSpaceObserver_.EnteredTrack += InOutHandler_.OnEnteredTrack;
            inAirSpaceObserver_.LeavingTrack += InOutHandler_.OnLeavingTrack;
            inAirSpaceObserver_.AirspaceUpdated += separationControl_.OnAirspaceUpdated;
            fakeTrackFactory_.TrackCreated += inAirSpaceObserver_.OnTrackCreated;
        }

        #region InOutTrack

        

          
        [Test]
        public void OnEnteredTrack_TestEvntcalledOnes()
        {
            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");
            newList.Add(inserTrack);

            int _evenCounter = 0;

            InOutHandler_.listInUpdated += (o, arg) => { _evenCounter++; };

            inAirSpaceObserver_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack));

            Assert.That(newList.Count, Is.EqualTo(1));

        }
      
        
                [Test]
                public void OnEnteredTrack_TestEvntcalledTheeTimes()
                {
                   
                    List<Track> newList = new List<Track>();
                    Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12050, 10, 34, "2016111912343892");
                    Track inserTrack1 = new Track("MCJ523", new Position(15000, 13002), 12400, 10, 34, "2016111932343892");
                    Track inserTrack2 = new Track("MCJ523", new Position(15000, 13040), 12300, 10, 34, "2016111945343892");
                    newList.Add(inserTrack);
                    newList.Add(inserTrack1);
                    newList.Add(inserTrack2);

                    //int _evenCounter = 0;
                    //uut_.listInUpdated += (o, arg) => { _evenCounter++; };

                    inAirSpaceObserver_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack));
                    inAirSpaceObserver_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack1));
                    inAirSpaceObserver_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack2));

                    Assert.That(newList.Count, Is.EqualTo(3));
                }
        /*

                        [Test]
                        public void OnLeavingTrack_TestEvntcalledOnes()
                        {

                            List<Track> newList = new List<Track>();
                            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");
                            newList.Add(inserTrack);

                            int _evenCounter = 0;

                            uut_.listOutUpdated += (o, arg) => { _evenCounter++; };

                            inAirSpaceObserver_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack));

                            Assert.That(_evenCounter, Is.EqualTo(newList.Count));

                        }

                        [Test]
                        public void OnLeavingTrack_TestEvntcalledTree()
                        {

                            List<Track> newList = new List<Track>();
                            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12050, 10, 34, "2016111912343892");
                            Track inserTrack1 = new Track("MCJ523", new Position(15000, 13002), 12400, 10, 34, "2016111932343892");
                            Track inserTrack2 = new Track("MCJ523", new Position(15000, 13040), 12300, 10, 34, "2016111945343892");
                            newList.Add(inserTrack);
                            newList.Add(inserTrack1);
                            newList.Add(inserTrack2);

                            int _evenCounter = 0;

                            uut_.listOutUpdated += (o, arg) => { _evenCounter++; };

                            inAirSpaceObserver_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack));
                            inAirSpaceObserver_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack1));
                            inAirSpaceObserver_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack2));

                            Assert.That(_evenCounter, Is.EqualTo(newList.Count));

                        }
                        */

        #endregion


        #region Separation

        [Test]
        public void OnAirSpaceUpdated_NoSeparationInList_NoDangersMade()
        {
            //Arrange
            Track[] _testTracks = new Track[]
            {
                //new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520412"),
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124520412"),
                //new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "20180304124520412"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "20180304124520412"),
                new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304124520412")
            };

            int eventCounter = 0;


            separationControl_.DangerListUpdated += (o, args) => { eventCounter++; };






            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));
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
        separations: 1-2,3-5

        */



        #endregion





    }
}
