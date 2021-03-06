﻿using System;
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
    public class BUStep3_InOutTrack
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
            trackOpticsProvider_ = new TrackOpticsProvider();
            inAirSpaceObserver_ = new InAirSpaceObserver(trackOpticsProvider_);
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
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "20180304124520412");

            int _evenCounter = 0;

            InOutHandler_.listInUpdated += (o, args) =>
            {
                _evenCounter++;
                newList = args.listEntered;
            };

            fakeTrackFactory_.TrackCreated += Raise.EventWith(this, new TrackArgs(inserTrack));

            Assert.That(_evenCounter, Is.EqualTo(1));
            Assert.That(_evenCounter, Is.EqualTo(newList.Count));
            Assert.That(newList[0] == inserTrack, Is.True);
        }

        [Test]
        public void OnEnteredTrack_TestEvntcalledTheeTimes()
        {
            List<Track> newList = new List<Track>();
            Track[] _testTracks = new Track[]
            {
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520412"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520412"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124520412")
            };

            int _evenCounter = 0;

            InOutHandler_.listInUpdated += (o, args) =>
               {
                   _evenCounter++;
                   newList = args.listEntered;
               };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated +=
                    Raise.EventWith(this, new TrackArgs(track));

            }

            Assert.That(_evenCounter, Is.EqualTo(newList.Count));
            Assert.That(_evenCounter, Is.EqualTo(3));
            Assert.That(newList.Count, Is.EqualTo(3));

        }

        
                [Test]
                public void OnLeavingTrack_TestEvntcalledOnes()
                {

                    List<Track> newList = new List<Track>();
                    Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 0, 0, "20161119123438924");
                    Track inserTrack2 = new Track("MCJ523", new Position(15000, 9000), 12000, 0, 0, "20161119123938925");

                    int _evenCounter = 0;

                    InOutHandler_.listOutUpdated += (o, args) =>
                    {
                        _evenCounter++;
                        newList = args.listLeft;
                    };

                    fakeTrackFactory_.TrackCreated +=
                        Raise.EventWith(this, new TrackArgs(inserTrack));
                    fakeTrackFactory_.TrackCreated +=
                        Raise.EventWith(this, new TrackArgs(inserTrack2));

                    Assert.That(_evenCounter, Is.EqualTo(newList.Count));
                    Assert.That(_evenCounter, Is.EqualTo(1));
                    Assert.That(newList.Count, Is.EqualTo(1));

                }
        
             
                [Test]
                public void OnLeavingTrack_TestEvntcalledTree()
                {
                    List<Track> newList = new List<Track>();
                    Track[] _testTracks = new Track[]
                    {
                        new Track("JjgfK742", new Position(89000, 21000), 800, 0, 0, "201804041245204124"),
                        new Track("SYjg871", new Position(60500, 71000), 550, 0, 0, "201804041246204124"),
                        new Track("PQki842", new Position(12000, 57432), 1648, 0, 0, "201804041247204124"),
                        new Track("JjgfK742", new Position(91000, 11000), 800, 0, 0, "201804041248204124"),
                        new Track("SYjg871", new Position(50500, 91000), 550, 0, 0, "201804041249204124"),
                        new Track("PQki842", new Position(9000, 67432), 1648, 0, 0, "201804041250204124")
                    };

                    int _evenCounter = 0;



                    InOutHandler_.listOutUpdated += (o, args) =>
                   {
                       _evenCounter++;
                       newList = args.listLeft;
                   };

                    foreach (var track in _testTracks)
                    {
                        fakeTrackFactory_.TrackCreated +=
                            Raise.EventWith(this, new TrackArgs(track));

                    }

                    Assert.That(_evenCounter, Is.EqualTo(newList.Count));
                    Assert.That(_evenCounter, Is.EqualTo(3));
                    Assert.That(newList.Count, Is.EqualTo(3));

                }
                
        


        #endregion


        #region Separation

        [Test]
        public void OnAirSpaceUpdated_NoSeparationInList_NoDangersMade()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            Track[] _testTracks = new Track[]
            {
                //new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304125040414"),
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "201803041245204124"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "201803041245204124"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "201803041245204124"),
                //new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "201803041245204124"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "201803041245204124"),
                new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "201803041245204124")
            };

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            separationControl_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));
            Assert.That(Dangerlist.Count, Is.EqualTo(0));

        }


        [Test]
        public void OnAirSpaceUpdated_1SeparationInList_1DangersMade()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            Track[] _testTracks = new Track[]
            {
                new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "201803041245204124"),
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "201803041245204124"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "201803041245204124"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "201803041245204124"),
                //new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "201803041245204124"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "201803041245204124"),
                //new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304125040414")
            };
            Danger testdanger = new Danger(_testTracks[0], _testTracks[1], 1802);

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            separationControl_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            //Assert
            Assert.That(eventCounter, Is.EqualTo(1));
            Assert.That(Dangerlist.Count, Is.EqualTo(1));
            Assert.That(Dangerlist[0] == testdanger, Is.True);

        }


        [Test]
        public void OnAirSpaceUpdated_XYSeparation_NotInAltitude_NoDangersMade()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            Track[] _testTracks = new Track[]
            {
                new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "201803041245204124"),
                new Track("JASK742", new Position(25000, 12500), 8000, 0, 0, "201803041245204124"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "201803041245204124"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "201803041245204124"),
                //new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "201803041245204124"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "201803041245204124"),
                //new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304125040414")
            };

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            separationControl_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));
            Assert.That(Dangerlist.Count, Is.EqualTo(0));

        }

        [Test]
        public void OnAirSpaceUpdated_Separation_NotInAltitude_NoDangersMade()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            Track[] _testTracks = new Track[]
            {
                //new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304125040414"),
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "201803041245204124"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "201803041245204124"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "201803041245204124"),
                //new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520412"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "201803041245204124"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "201803041245204124"),
                new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "201803041245204124")
            };

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            separationControl_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            //Assert
            Assert.That(eventCounter, Is.EqualTo(0));
            Assert.That(Dangerlist.Count, Is.EqualTo(0));

        }



        [Test]
        public void OnAirSpaceUpdated_make1Danger_Remove1Danger()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            Track[] _testTracks = new Track[]
            {
                new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520414"),
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520414"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124520414"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124520414"),
                new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "20180304124520414"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "20180304124520414"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "20180304124520414"),
                new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304125040414")
            };

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            separationControl_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            //Assert
            Assert.That(eventCounter, Is.EqualTo(2));
            Assert.That(Dangerlist.Count, Is.EqualTo(0));

        }

        [Test]
        public void OnAirSpaceUpdated_make2Danger_update1Danger()
        {
            //Arrange
            List<Danger> Dangerlist = new List<Danger>();
            Track[] _testTracks = new Track[]
            {
                new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "20180304124520414"),
                new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "20180304124520414"),
                new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "20180304124720414"),
                new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "20180304124720414"),
                new Track("WUAX143", new Position(55200, 64500), 500, 0, 0, "20180304124820414"),
                new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "20180304124820414"),
                new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "20180304124920414"),
                new Track("HSAN329", new Position(24500, 12000), 600, 0, 0, "20180304125040414")
            };

            int eventCounter = 0;

            // Lambda expression, what happens at event in test.
            separationControl_.DangerListUpdated += (o, args) =>
            {
                eventCounter++;
                Dangerlist = args.DangerList_;
            };
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            //Assert
            Assert.That(eventCounter, Is.EqualTo(3));
            Assert.That(Dangerlist.Count, Is.EqualTo(2));


        }



        /*
        //Arrange
        Track track1 = new Track("HSAN329", new Position(24000, 11000), 550, 0, 0, "201803041245204124");
        Track track2 = new Track("JASK742", new Position(25000, 12500), 800, 0, 0, "201803041245204124");
        Track track3 = new Track("SYMS871", new Position(54050, 64800), 550, 0, 0, "201803041245204124");
        Track track4 = new Track("PQAS842", new Position(12400, 67842), 1648, 0, 0, "201803041245204124");
        Track track5 = new Track("WUAX143", new Position(55200, 64500), 15340, 0, 0, "201803041245204124");
        Track track6 = new Track("CLAR274", new Position(65740, 11000), 6700, 0, 0, "201803041245204124");
        Track track7 = new Track("AIAS527", new Position(30000, 24500), 900, 0, 0, "201803041245204124");
        Track track8 = new Track("HSAN329", new Position(38000, 30000), 4000, 0, 0, "20180304125040414");
        separations: 1-2,3-5

        */



        #endregion





    }
}
