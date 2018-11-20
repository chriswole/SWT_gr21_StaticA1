//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Runtime.Remoting.Channels;
//using ATM_App2.Classes;
//using NUnit.Framework;
//using NSubstitute;
//using ATM_App2.Events;
//using ATM_App2.Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Assert = NUnit.Framework.Assert;


//namespace ATM_App2_UnitTest.UnitTestEvents
//{
//    [TestFixture]
//    [Author("Mette Jacobsen, Edited by members of group 21")]
//    [TestClass]
//    public class UnitTestTrackEnteredAirspace
//    {
//        private TrackEnteredAirspace _uut;
//        private ILogToFile _logToFile;
//        private Track _data1;
//        private Track _data2;
//        private IInAirSpaceObserver fakeAirSpace_;

//        [SetUp]
//        public void Setup()
//        {
//            _uut = new TrackEnteredAirspace(_logToFile = Substitute.For<ILogToFile>());
//            //_data1 = new Track("plane1", new Position(20000, 50000), 10000, 50000, 56, "1234");
//            // _data2 = new Track("plane2");
//            // fakeTrack_.EnteredTrack += _uut.OnEnteredTrack;
//            fakeAirSpace_.EnteredTrack += _uut.OnEnteredTrack;
//        }
//        [Test]
//        public void DetectNotification_NoData_NoEvent()
//        {
//            int notificationEventCalled = 0;
//            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

//            _uut.DetectNotification(new Collection<Track>(), new Collection<Track>());
//            Assert.That(notificationEventCalled, Is.EqualTo(0));
//        }

//        [Test]
//        public void DetectNotification_OneEnters_OneEvent()
//        {
//            int notificationEventCalled = 0;
//            ATMNotification.NotificationEvent += (sender, args) =>
//            {
//                if (args.Tag == "plane1" && args.EventName == "TrackEnteredAirspace")
//                    notificationEventCalled++;
//            };
//            _uut.DetectNotification(new Collection<Track>(), new Collection<Track> { _data1 });
//            Assert.That(notificationEventCalled, Is.EqualTo(1));
//        }
//        [Test]
//        public void DetectNotification_ThreeEnters_ThreeEvent()
//        {
//            int notificationEventCalled = 0;
//            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

//            _uut.DetectNotification(new Collection<Track>(), new Collection<Track>() { _data1, _data2, _data1 });
//            Assert.That(notificationEventCalled, Is.EqualTo(3));
//        }

//        [Test]
//        public void DetectNotification_ExistingFlight_NoEvent()
//        {
//            int notificationEventCalled = 0;
//            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

//            _uut.DetectNotification(new Collection<Track> { _data1 }, new Collection<Track> { _data1 });
//            Assert.That(notificationEventCalled, Is.EqualTo(0));
//        }

//        [Test]
//        public void DetectNotification_OneTrackEntereds_EventLoggedOnce()
//        {
//            _uut.DetectNotification(new Collection<Track>(), new Collection<Track> { _data1 });
//            _logToFile.Received(1).Log("1234 TrackEnteredAirspace Notification plane1");
//        }
//        [Test]
//        public void DetectNotification_OneTrackEntereds_EventLoggedOnce1()
//        {
//            List<Track> newList = new List<Track>();
//            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");
//            newList.Add(inserTrack);
//            int _evenCounter = 0;
//            _logToFile.Received(1).Log("2016111912343892 TrackEnteredAirspace Notification MCJ523");
//            _uut.listInUpdated+= (o, arg) => { _evenCounter++;};
//        }
//    }
//}

