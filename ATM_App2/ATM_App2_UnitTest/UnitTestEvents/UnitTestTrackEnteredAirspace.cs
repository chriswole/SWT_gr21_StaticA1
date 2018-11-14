/*using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Channels;
using ATM_App2.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using ATM_App2.Events;
using ATM_App2.Interfaces;


namespace ATM_App2_UnitTest.UnitTestEvents
{
    [TestFixture]
    [Author("Mette Jacobsen, Edited by members of group 21")]
    [TestClass]
    public class UnitTestTrackEnteredAirspace
    {
        private TrackEnteredAirspace _uut;
        private ILogToFile _logToFile;
        private Track _data1;
        private Track _data2;

        [SetUp]
        public void Setup()
        {
            _uut = new TrackEnteredAirspace(_logToFile = Substitute.For<LogToFile>());
            _data1 = Substitute.For<Track>();
            _data2 = Substitute.For<Track>();
            //_data1.tag_ = "plane1";
            //_data2.tag_ = "plane2";
            //_data1.timestamp_ = "1234";

        }
        [Test]
        public void DetectNotification_NoData_NoEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

            _uut.DetectNotification(new Collection<Track>(), new Collection<Track>());
            Assert.That(notificationEventCalled, Is.EqualTo(0));
        }

        [Test]
        public void DetectNotification_OneEnters_OneEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) =>
            {
                if (args._tag == "Plane1" && args.EventName == "TrackEnteredAirspace")
                    notificationEventCalled++;
            };
            _uut.DetectNotification(new Collection<Track>(), new Collection<Track> { _data1 });
            Assert.That(notificationEventCalled, Is.EqualTo(1));
        }
        [Test]
        public void DetectNotification_ThreeEnters_ThreeEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

            _uut.DetectNotification(new Collection<Track>(), new Collection<Track>() { _data1, _data2, _data1 });
            Assert.That(notificationEventCalled, Is.EqualTo(3));
        }

        [Test]
        public void DetectNotification_ExistingFlight_NoEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

            _uut.DetectNotification(new Collection<Track> { _data1 }, new Collection<Track> { _data1 });
            Assert.That(notificationEventCalled, Is.EqualTo(0));
        }

        [Test]
        public void DetectNotification_OneTrackEntereds_EventLoggedOnce()
        {
            _uut.DetectNotification(new Collection<Track>(), new Collection<Track> { _data1 });
            _logToFile.Received(1).Log("1221 TrackEnteredAirspace Notification item1");
        }
    }
}
*/
