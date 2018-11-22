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
        private InOutTrack uut_;
        private IInAirSpaceObserver inAirSpaceObserver_;
        private ITrackOpticsProvider trackOpticsProvider_;
        private ILogToFile atmLog_;

        [SetUp]
        public void Setup()
        {
            atmLog_ = new LogToFile();
            trackOpticsProvider_=new TrackOpticsProvider();
            inAirSpaceObserver_=new InAirSpaceObserver(trackOpticsProvider_);
            uut_=new InOutTrack(atmLog_);

            inAirSpaceObserver_.EnteredTrack += uut_.OnEnteredTrack;
            inAirSpaceObserver_.LeavingTrack += uut_.OnLeavingTrack;
        }
        [Test]
        public void OnEnteredTrack_TestEvntcalledOnes()
        {
            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");
            newList.Add(inserTrack);

            int _evenCounter = 0;

            uut_.listInUpdated += (o, arg) => { _evenCounter++; };

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
    }
}
